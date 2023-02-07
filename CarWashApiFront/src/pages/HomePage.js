import { makeStyles } from "@material-ui/core/styles";
import React, { useEffect, useState } from "react";
import { useTheme } from "@material-ui/styles";
import Grid from "@material-ui/core/Grid";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";
import Button  from "@material-ui/core/Button";
import Collapse  from "@material-ui/core/Collapse";

import CustomDialog from "../component/ui/CustomDialog";
import HomePic from "../pictures/image4.jpg";


const useStyles = makeStyles((theme) => ({
  PictureBlock: {
    width: "100%",
    height: "60rem",
    backgroundImage: `url(${HomePic})`,
  },
  Table: {
    width: "50rem",
    height: "20rem",
  },
  table: {
    backgroundColor: "#9EA9A9",
  },
  HeadTable: {
    
  },
  paper: {
    position: "absolute",
    left: "25rem",
    bottom: "15rem",
    width: 400,
    backgroundColor: theme.palette.background.paper,
    border: "2px solid #000",
    boxShadow: theme.shadows[5],
    padding: theme.spacing(2, 4, 3),
  },
}));

const DUMMY_carwash = [
  {
    services: [
      {
        id: 1,
        typeOfService: "Regular",
        price: "200"
      },
      {
        id: 2,
        typeOfService: "Extended",
        price: "300"
      },
      {
        id: 3,
        typeOfService: "Premium",
        price: "400"
      }
    ],
    id: 1,
    shopName: "Megawash",
    address: "13 Augusta St.Yuma,AZ 85365",
    openingTime: 9,
    closingTime: 22
  },
  {
    services: [
      {
        id: 1,
        typeOfService: "Regular",
        price: "200"
      },
      {
        id: 2,
        typeOfService: "Extended",
        price: "300"
      },
      {
        id: 3,
        typeOfService: "Premium",
        price: "400"
      }
    ],
    id: 2,
    shopName: "Superwash",
    address: "6 White StreetBrick,NJ 08723",
    openingTime: 8,
    closingTime: 22
  },
  {
    services: [
      {
        id: 1,
        typeOfService: "Regular",
        price: "200"
      },
      {
        id: 2,
        typeOfService: "Extended",
        price: "300"
      },
      {
        id: 3,
        typeOfService: "Premium",
        price: "400"
      }
    ],
    id: 3,
    shopName: "Sparcle Clean",
    address: "2 10th St.Bear,DE 19701",
    openingTime: 8,
    closingTime: 20
  },
  {
    services: [
      {
        id: 1,
        typeOfService: "Regular",
        price: "200"
      },
      {
        id: 2,
        typeOfService: "Extended",
        price: "300"
      },
      {
        id: 3,
        typeOfService: "Premium",
        price: "400"
      }
    ],
    id: 4,
    shopName: "Wash me Now",
    address: "75 Hill Rd.Coventry,RI 02816",
    peningTime: 7,
    closingTime: 19
  },
  {
    services: [
      {
        id: 1,
        typeOfService: "Regular",
        price: "200"
      },
      {
        id: 2,
        typeOfService: "Extended",
        price: "300"
      },
      {
        id: 3,
        typeOfService: "Premium",
        price: "400"
      }
    ],
    id: 5,
    shopName: "Platinum Auto Spa",
    address: "911 Saxon St.Chaska,MN 55318",
    openingTime: 9,
    closingTime: 17
  }
];
const test_carWashes = [
  {
    id: 1,
    name: "MegaCarWash",
    owner: "Misa",
    services: [
      {
        serviceName: "Regular",
        servicePrice: 100,
      },
      {
        serviceName: "Extended",
        servicePrice: 200,
      },
      {
        serviceName: "Premium",
        servicePrice: 300,
      },
    ],
  },
  {
    id: 2,
    name: "Super CarWash",
    owner: "Misa",
    services: [
      {
        serviceName: "Regular",
        servicePrice: 100,
      },
      {
        serviceName: "Extended",
        servicePrice: 200,
      },
      {
        serviceName: "Premium",
        servicePrice: 300,
      },
    ],
  },
  {
    id: 3,
    name: "Giga CarWash",
    owner: "Pera",
    services: [
      {
        serviceName: "Regular",
        servicePrice: 100,
      },
      {
        serviceName: "Extended",
        servicePrice: 200,
      },
      {
        serviceName: "Premium",
        servicePrice: 300,
      },
    ],
  },
  {
    id: 4,
    name: "Zika CarWash",
    owner: "Zika",
    services: [
      {
        serviceName: "Regular",
        servicePrice: 100,
      },
      {
        serviceName: "Extended",
        servicePrice: 200,
      },
      {
        serviceName: "Premium",
        servicePrice: 300,
      },
    ],
  },
];

export default function HomePage(props) {
  const classes = useStyles();
  const theme = useTheme();

  

  const [carwashes, setCarWashes] = useState([]);

  const [openDialog, setOpenDialog] = useState(false);
  const [currentCarWash, setCurrentCarWash] = useState({});

  //const [open, setOpen] = useState();

  // const handleOpen = () => {
  //   setOpen(true);
  // };

  // const handleClose = () => {
  //   setOpen(false);
  // };
  useEffect(() => {
    const http = async () => {
      const response = await fetch("https://localhost:7143/getShops", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("token")
        },
      })

      if(!response.ok){
        const log = await response.json();
        throw new Error(log);
      }
      
      return await response.json();
    }
    
    const httpCall = async () =>{
      try{
        const response = await http();
        setCarWashes(response);
      }
      catch(error){
        console.log(error);
      }
    }

    httpCall();
    
  },[])
  return (
    <React.Fragment>
      <Grid container direction="column" justify="center" alignItems="center">
        <Grid item container className={classes.PictureBlock}>
          <Grid container justify="center" alignItems="center">
            <Grid item className={classes.Table}>
              <TableContainer component={Paper}>
                <Table className={classes.table} aria-label="simple table">
                  <TableHead className={classes.HeadTable}>
                    <TableRow >
                      <TableCell align="left">Car Wash Shops</TableCell>
                      <TableCell align="left">Address</TableCell>
                      <TableCell align="left">Opening Time</TableCell>
                      <TableCell align="left">Closing Time</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {carwashes.map((carwash) => (
                      <><TableRow key={carwash.id}>
                        <TableCell>{carwash.shopName}</TableCell>
                        <TableCell>{carwash.address}</TableCell>
                        <TableCell>{carwash.openingTime}</TableCell>
                        <TableCell>{carwash.closingTime}</TableCell>
                        {/* <TableCell>
                          {carwash.services
                            .map((service) => service.typeOfService)
                            .join(" ")}
                        </TableCell> */}
                        <TableCell>
                          <Button
                            onClick={() => {
                              setCurrentCarWash(carwash);
                              setOpenDialog(true);
                              
                            }}
                          >
                            Schedule
                          </Button>
                        </TableCell>
                      </TableRow>
                      </>
                    ))}
                  </TableBody>
                </Table>
                <CustomDialog open={openDialog} setOpen={setOpenDialog} shop={currentCarWash} />
              </TableContainer>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
}
