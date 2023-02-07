import React, { useState, useEffect, useContext } from "react";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import { makeStyles } from "@material-ui/styles";
import Tab from "@material-ui/core/Tab";
import Tabs from "@material-ui/core/Tabs";
import Button from "@material-ui/core/Button";
import { Link } from "react-router-dom";
import useMediaQuery from "@material-ui/core/useMediaQuery";
import { useTheme } from "@material-ui/core/styles";

import Menu from "@material-ui/core/Menu";
import MenuItem from "@material-ui/core/MenuItem";

import header from "../../pictures/header.svg";
import logo from "../../pictures/logo.wash.png";
import AuthContext from "../../context/Auth-context";

const useStyles = makeStyles((theme) => ({
  toolbarMargin: {
    ...theme.mixins.toolbar,
    marginBottom: "3em",
    [theme.breakpoints.down("md")]: {
      marginBottom: "2em",
    },
    [theme.breakpoints.down("xs")]: {
      marginBottom: "0.1em",
    },
  },

  appBar: {
    backgroundImage: `url(${header})`,
    height: "7rem",
    backgroundSize: "cover",
    backgroundPosition: "center",
    backgroundRepeat: "no-repeat",
    backgroundColor: theme.palette.common.blue,

    [theme.breakpoints.down("md")]: {
      height: "8em",
    },
    [theme.breakpoints.down("xs")]: {
      height: "4em",
    },
  },
  tabContainer: {
    marginLeft: "auto",
  },
  tab: {
    fontFamily: "Roboto",
    fontWeight: 700,
    color: "#FFF718",
    textTransform: "none",
    textDecoration: "none",
    minWidth: 10,
    marginLeft: "25px",
  },
  button: {
    color: "#000",
    backgroundColor: "#FFF718",
    borderRadius: "50px",
    marginLeft: "50px",
    marginRight: "25px",
    textTransform: "none",
    height: "45px",
  },
  menuButton: {
    fontFamily: "Roboto",
    fontWeight: 700,
    marginLeft: "5px",
    marginRight: "10px",
    color: "#FFF718",
    "&:hover" : {
      backgroundColor : "transparent"
    }
  },
  logo: {
    width: "30rem",
    position: "absolute",
    top: -170,
    left: "-7rem",
    height: "500px",
  },
  logoContainr: {
    top: "-1.6em",
  },
}));

export default function Header(props) {
  const classes = useStyles();
  const [anchorEl, setAnchorEl] = useState(null);
  const { value, setValue, selectedIndex, setSelectedIndex } = props;
  const authCtx = useContext(AuthContext);
  const {onLogout} = authCtx; 

  const theme = useTheme();
  const matches = useMediaQuery(theme.breakpoints.down("md"));

  const handlerChange = (e, Value) => {
    setValue(Value);
  };

  const handlerClick = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handlerClose = () => {
    setAnchorEl(null);
  };

  const routes = [
    { name: "Home", link: "/", activeIndex: 0 },
    { name: "Login", link: "/login", activeIndex: 1 },
    { name: "Profile", link: "/Profile", activeIndex : 2}
  ];

  useEffect(() => {
    [...routes].forEach((route) => {
      switch (window.location.pathname) {
        case `${route.link}`:
          if (value !== route.activeIndex) {
            setValue(route.activeIndex);
            if (route.selectedIndex && route.selectedIndex !== selectedIndex) {
              setSelectedIndex(route.selectedIndex);
            }
          }
          break;
        default:
          break;
      }
    });
  }, [value, selectedIndex, routes, setSelectedIndex, setValue]);

  // useEffect(() => {
  //   if (window.location.pathname === "/" && value !==0) {
  //       setValue(0)
  //   }  else  if (window.location.pathname === "/shop" && value !==1) {
  //      setValue(1)
  //   } else  if (window.location.pathname === "/about" && value !==2) {
  //       setValue(2)
  //   } else  if (window.location.pathname === "/contact" && value !==3) {
  //       setValue(3)
  //   } else  if (window.location.pathname === "/reservation" && value !==4) {
  //       setValue(4)
  //   }
  //   }, [value, setValue])

  const tabs = (
    <React.Fragment>
      <Tabs
        value={value}
        indicatorColor="primary"
        onChange={handlerChange}
        className={classes.tabContainer}
      >
        {routes.map((route, index) => (
          <Tab
            key={`${route}${index}`}
            className={classes.tab}
            component={Link}
            to={route.link}
            label={route.name}
            aria-owns={route.ariaOwns}
            aria-haspopup={route.ariaPopup}
            onMouseOver={route.mouseOver}
          />
        ))}
      </Tabs>
      <Button
        aria-controls="simple-menu"
        aria-haspopup="true"
        disableRipple
        onClick={handlerClick}
        className={classes.menuButton}
      >
        Menu
      </Button>
    </React.Fragment>
  );

  return (
    <React.Fragment>
      <AppBar position="fixed" className={classes.appBar}>
        <Toolbar disableGutters>
          <Button
            component={Link}
            to="/"
            disableRipple
            onClick={() => props.setValue(0)}
            className={classes.logoContainr}
          >
            <img alt="logo designe" src={logo} className={classes.logo} />
          </Button>
          {matches ? null : tabs}

          <Menu
            id="simple-menu"
            anchorEl={anchorEl}
            keepMounted
            open={Boolean(anchorEl)}
            onClose={handlerClose}
          >

            <MenuItem onClick={onLogout}>Logout</MenuItem>
          </Menu>
        </Toolbar>
      </AppBar>
      <div className={classes.toolbarMargin} />
    </React.Fragment>
  );
}
