import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Grid from "@material-ui/core/Grid";
import {Link} from "react-router-dom";

const useStyles = makeStyles((theme) => ({
  footer: {
    backgroundColor: theme.palette.common.blue,
    width: "100%",
    height: "10rem",
    zIndex: 1302,
    position: "relative",
    
    
  },
  adorment: {
    width: "100%",
    height: "20rem",
    verticalAlign: "bottom",
    margin: "auto",
    
  },

  mainContainer: {
    position: "absolute"
  },
  link: {
    color: "#FFF718",
    fontFamily: "Roboto",
    fontSize: "0.9rem",
    fontWeight: "bold",
    marginLeft: "5px",
    textDecoration: "none"
  },
  gridItem: {
    margin: "2em"
  }
}));

export default function Footer(props) {
  const classes = useStyles();

  return (
    <footer className={classes.footer} >
      <Grid container  justify="center" className={classes.mainContainer}>
        <Grid item className={classes.gridItem}>
          <Grid container direction="column" >
            <Grid item component={Link}  to="/" className={classes.link}>
              Home
              </Grid>
          </Grid>
        </Grid>
        <Grid item className={classes.gridItem}>
          <Grid container direction="column" >
            <Grid item component={Link} to="/Login" className={classes.link}>
              Login
            </Grid>
          </Grid>
        </Grid>
        <Grid item className={classes.gridItem}>
          <Grid container direction="column" >
            <Grid item component={Link} to="/Profile" className={classes.link}>
              Profile
            </Grid>
          </Grid>
        </Grid>
      </Grid>      
    </footer>
  );
}
