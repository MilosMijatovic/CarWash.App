import React from "react";
import { makeStyles } from "@material-ui/styles";
import Grid from "@material-ui/core/Grid";
import { useTheme } from "@material-ui/styles";


const useStyles = makeStyles((theme) => ({
  
}));

export default function ShopPage(props) {
  const classes = useStyles();
  const theme = useTheme();

  return (
    <React.Fragment>
      <Grid container direction="column" justify="center" alignItems="center">
        <Grid item container >
        </Grid>
      </Grid>
    </React.Fragment>
  );
}