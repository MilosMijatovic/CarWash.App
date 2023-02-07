import React, { useEffect, useState } from "react";
import { makeStyles } from "@material-ui/styles";
import Grid from "@material-ui/core/Grid";
import { useTheme } from "@material-ui/styles";
import Link from "@material-ui/core/Link";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import { Button } from "@material-ui/core";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";

function createData(id, date, shopName, service) {
  return { id, date, shopName, service };
}

const rows = [
  createData(0, "1 Mar, 2019", "Megawash", "Premium"),
  createData(1, "10 Mar, 2019", "Superwash", "Extended"),
  createData(2, "15 Mar, 2019", "SparcleClean", "Standard"),
  createData(3, "20 Mar, 2019", "Megawash", "Standard"),
  createData(4, "25 Mar, 2019", "Superwash", "Premium"),
];

function preventDefault(event) {
  event.preventDefault();
}

const useStyles = makeStyles((theme) => ({
  seeMore: {
    marginTop: theme.spacing(3),
  },
  historyTable: {
    backgroundColor: "#1D98DF",
    width: "40rem",
    borderRadius: "0.5rem",
  },
  Table: {
    position: "relative",
    marginTop: "15rem",
    marginBottom: "5rem",
    margin: theme.spacing(3),
    backgroundColor: "",
  },
  seeMore: {
    marginBottom: 20,
    marginLeft: 20,
    marginTop: 10,
  },
  Button: {
    backgroundColor: "gray",
  },
  root: {
    width: "20rem",
    height: "15rem",
  },
  Card: {
    marginTop: "5rem",
  },
  title: {
    marginLeft: "5.5rem",
    marginBottom: "1.5rem",
    fontSize: 25,
  },
  nameInfo: {
    marginLeft: "4rem",
    marginBottom: "1rem",
  },
  pos: {
    marginBottom: 12,
  },
  mailInfo: {
    marginTop: "2rem",
    marginLeft: 20,
  },
}));

export default function ProfilePage(props) {
  const classes = useStyles();
  const theme = useTheme();

  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit = (event) => {
    event.preventDefault();

    setIsLoading(true);
    // let url;
    // if (isDeleting) {
    //   url = "https://localhost:7143/Cancel/";
    // }
    // fetch(url, {
    //   method: "DELETE",
    //   body: JSON.stringify(Dti),
    //   headers: {
    //     "Content-Type": "application/json",
    //   },
    // })
    //   .then((res) => {
    //
    //     if (res.ok) {
    //       return res.json();
    //     } else {
    //       return res.json().then((data) => {
    //         let errorMessage = "Authentication failed";
    //         // if(data && data.error && data.error.message) {
    //         //   errorMessage = data.error.message;
    //         // }

    //         throw new Error(errorMessage);
    //       });
    //     }
    //   })
    //   .then((data) => {
    //     console.log(data);
    //     setIsLoading(false);
    //   })
    //   .catch((err) => {
    //     alert(err.message);
    //   });
  };

 

  return (
    <React.Fragment>
      <Grid container direction="column" justify="center" alignItems="center">
        <Grid item container justify="center" className={classes.Card}>
          <Card className={classes.root} elevation={20}>
            <CardContent>
              <Typography className={classes.title} gutterBottom>
                {" "}
                User Info
              </Typography>
              <Typography
                className={classes.nameInfo}
                variant="h5"
                component="h2"
              >
                Milos Mijatovic
              </Typography>
              <Typography
                className={classes.pos}
                color="textSecondary"
              ></Typography>
              <Typography
                className={classes.mailInfo}
                variant="body2"
                component="p"
              >
                email:
                <br />
                {'"milos@gmail.com"'}
              </Typography>
            </CardContent>
          </Card>
          <Grid container justify="center" className={classes.Table}>
            <Grid item className={classes.historyTable}>
              <Table size="small">
                <TableHead>
                  <TableRow>
                    <TableCell>Date</TableCell>
                    <TableCell>Shop Name</TableCell>
                    <TableCell>Service</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {rows.map((row) => (
                    <TableRow key={row.id}>
                      <TableCell>{row.date}</TableCell>
                      <TableCell>{row.shopName}</TableCell>
                      <TableCell>{row.service}</TableCell>
                      <TableCell>
                        <Button className={classes.Button}>Cancel</Button>
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
              <div className={classes.seeMore}>
                <Link color="primary" href="#" onClick={preventDefault}>
                  See more
                </Link>
              </div>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
}
