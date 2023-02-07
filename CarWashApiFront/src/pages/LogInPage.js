import React, { useState, useRef, Fragment, useContext } from "react";
import Typography from "@material-ui/core/Typography";
import Box from "@material-ui/core/Box";
import Grid from "@material-ui/core/Grid";
import Button from "@material-ui/core/Button";
import Link from "@material-ui/core/Link";
import Avatar from "@material-ui/core/Avatar";
import TextField from "@material-ui/core/TextField"; 
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import Container from "@material-ui/core/Container";
import { useTheme } from "@material-ui/styles";
import { makeStyles } from "@material-ui/styles";
import { Paper, Select, MenuItem } from "@material-ui/core";
import AuthContext from "../context/Auth-context";

const useStyles = makeStyles((theme) => ({
  mainContainer: {
    zIndex: 1500,
    marginTop: "10rem",
    marginBottom: "10rem",
  },
  box: {
    padding: "25px 15px",
  },
  avatar: {
    marginLeft: "auto",
    marginRight: "auto",
    color: "blue",
  },
  passFor: {
    marginTop: "1rem",
  },
}));

export default function LogInPage(props) {
  const classes = useStyles();
  const theme = useTheme();

  const userNameInputRef = useRef();
  const passwordInputRef = useRef();
  const emailInputRef = useRef();
  const firstNameInputRef = useRef();
  const lastNameInputRef = useRef();
  const addressInputRef = useRef();
  const [isOwner, setIsOwner] = useState("");

  const [isLogin, setIsLogin] = useState(true);
  const [isLoading, setIsLoading] = useState(false);
  const authCtx = useContext(AuthContext);
  const {onLogin} = authCtx;


  const switchAuthModeHandler = () => {
    setIsLogin((prevState) => !prevState);
  };
  
  const handleSubmit = (event) => {
    event.preventDefault();
    
    const enteredUserName = userNameInputRef.current.value;
    const enteredPassword = passwordInputRef.current.value;
    let enteredFirstName;
    let enteredLastName;
    let enteredEmail;
    let enteredAddress;
    if (!isLogin) {
      enteredFirstName = firstNameInputRef.current.value;
      enteredLastName = lastNameInputRef.current.value;
      enteredEmail = emailInputRef.current.value;
      enteredAddress = addressInputRef.current.value;
    }

    const Dto = {
      userName: enteredUserName,
      password: enteredPassword,
      firstName: enteredFirstName,
      lastName: enteredLastName,
      emailAddress: enteredEmail,
      address: enteredAddress,
      isOwner: !!isOwner,
    };

  

    setIsLoading(true);
    let url;
    if (isLogin) {
      url = "https://localhost:7143/api/accounts/Login";
    } else {
      url = "https://localhost:7143/api/accounts/Create";
    }
    fetch(url, {
      method: "POST",
      body: JSON.stringify(Dto),
      headers: {
        "Content-Type": "application/json",
        
      },
    })
      .then((res) => {
        
        if (res.ok) {
          return res.json();
        } else {
          return res.json().then((data) => {
            let errorMessage = "Authentication failed";
            // if(data && data.error && data.error.message) {
            //   errorMessage = data.error.message;
            // }

            throw new Error(errorMessage);
          });
        }
      })
      .then((data) => {
        console.log(data);
        onLogin(data.token);
        setIsLoading(false);
      })
      .catch((err) => {
        alert(err.message);
      });
  };

  const formExtension = (
    <Fragment>
      <TextField
        margin="normal"
        required
        fullWidth
        id="firstname"
        label="First Name"
        name="firstname"
        autoComplete="firstname"
        autoFocus
        variant="outlined"
        inputRef={firstNameInputRef}
      />
      <TextField
        margin="normal"
        required
        fullWidth
        id="email"
        label="Email Address"
        name="email"
        autoComplete="email"
        autoFocus
        variant="outlined"
        inputRef={emailInputRef}
      />
      <TextField
        margin="normal"
        required
        fullWidth
        id="lastName"
        label="Last Name"
        name="lastName"
        autoComplete="off"
        autoFocus
        variant="outlined"
        inputRef={lastNameInputRef}
      />
      <TextField
        margin="normal"
        required
        fullWidth
        id="address"
        label="Address"
        name="address"
        autoComplete="off"
        autoFocus
        variant="outlined"
        inputRef={addressInputRef}
      />
        <Select value={isOwner} onChange={(e) =>{setIsOwner(e.target.value)}}>
            <MenuItem value={true}>Owner</MenuItem>
            <MenuItem value={false}>Consumer</MenuItem>
        </Select>
    </Fragment>
  );

  return (
    <Container className={classes.mainContainer} component="main" maxWidth="xs">
      <Paper
        elevation={10}
        className={classes.box}
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Avatar
          className={classes.avatar}
          sx={{ m: 1, bgcolor: "secondary.main" }}
        >
          <LockOutlinedIcon />
        </Avatar>
        <Typography align="center" component="h1" variant="h5">
          {isLogin ? "Login" : "Sing Up"}
        </Typography>
        <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            fullWidth
            id="userName"
            label="User Name"
            name="username"
            autoComplete="username"
            autoFocus
            variant="outlined"
            inputRef={userNameInputRef}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="password"
            label="Password"
            type="password"
            id="password"
            autoComplete="current-password"
            variant="outlined"
            inputRef={passwordInputRef}
          />
          {!isLogin && formExtension}
          {!isLoading && (
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              color="primary"
            >
              {isLogin ? "Login" : "Create Account"}
            </Button>
          )}
          {isLoading && <p>Sending request...</p>}
          <Grid container className={classes.passFor}>
            <Grid item>
              <Link href="#" variant="body2" onClick={switchAuthModeHandler}>
                {"Don't have an account? Sign Up"}
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Paper>
    </Container>
  );
}
