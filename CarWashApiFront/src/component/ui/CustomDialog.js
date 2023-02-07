import React, { useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogContentText from "@material-ui/core/DialogContentText";
import DialogTitle from "@material-ui/core/DialogTitle";
import Select from "@material-ui/core/Select";
import MenuItem from "@material-ui/core/MenuItem";
import TextField from "@material-ui/core/TextField";

const useStyles = makeStyles((theme) => ({
  dialogTitle: {
    height: "4rem",
    position: "apsolute",
  },
  dialogContent: {
    height: "3rem",
  },
  datetime: {
    
  }
}));

const CustomDialog = (props) => {
  const { open, setOpen, shop } = props;

  const [isBooking, setIsBooking] = useState(true);
  const [isLoading, setIsLoading] = useState(false);
  const [serviceValue, setServiceValue] = useState("");
  const [datetime, setDateTime] = useState("");
  const classes = useStyles();
  const closeHandler = () => {
    setOpen(false);
  };

  const handleChange = (event) => {
    setServiceValue(event.target.value);
  };
    const handleDateTime = (event) => {
      setDateTime(event.target.value);
    }

  const handleSubmit = (event) => {
    event.preventDefault();


    const enteredShopId = shop.id;
    const enteredServiceId = serviceValue;
    const enteredDateTime = datetime;

    const Dti = {
      shopId: enteredShopId,
      serviceId: enteredServiceId,
      reserved : enteredDateTime
    }
    console.log(Dti);

   setIsLoading(true);
    let url;
    if (isBooking) {
      url = "https://localhost:7143/Booking";
    }
    fetch(url, {
      method: "POST",
      body: JSON.stringify(Dti),
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
      })
      .catch((err) => {
        alert(err.message);
      });

    }

  return (
    <Dialog open={open} onClose={closeHandler}>
      <DialogTitle className={classes.dialogTitle} align="center">
        Schedule Appointment
      </DialogTitle>
      <DialogContent>
        <DialogContentText align="center" className={classes.dialogContent}>
          CarWash Name: {shop.shopName}
        </DialogContentText>
        <Select displayEmpty onChange={handleChange} value={serviceValue}>
          {shop.services !== undefined &&
            shop.services.map((service) => (
              <MenuItem
                key={service.id}
                value={service.id}
              >
                {service.typeOfService}
              </MenuItem>
            ))}
        </Select>
        <TextField className={classes.datetime} 
          id="datetime-local"
          label="Next Appointment"
          type="datetime-local"
          value={datetime}
          onChange={handleDateTime}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </DialogContent>
      <DialogActions>
        <Button
          onClick={() => {
            setOpen(false);
          }}
        >
          Close
        </Button>
        {(!isLoading && <Button onClick={handleSubmit}>{isBooking && "Schedule"}</Button>)}
      </DialogActions>
    </Dialog>
  );
};

export default CustomDialog;
