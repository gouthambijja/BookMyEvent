import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Alert from '@mui/material/Alert';
import AlertTitle from '@mui/material/AlertTitle';
import ErrorIcon from '@mui/icons-material/Error';

const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
    maxWidth:'800px',
    padding: theme.spacing(2),
    marginBottom: theme.spacing(2),
    display: 'flex',
    alignItems: 'center',
    backgroundColor: theme.palette.error.main,
    color: theme.palette.error.contrastText,
    borderRadius: theme.shape.borderRadius,
  },
  icon: {
    marginRight: theme.spacing(2),
  },
}));

const Error = ({ title="Error!", message="Something went wrong please reload the page or go to the previous page!" }) => {
  const classes = useStyles();

  return (
    <div style={{width:'100vw',background:'linear-gradient(grey,black)',height:'100vh',display:'flex',justifyContent:'center',alignItems:'center'}}>
    <div className={classes.root}>
      <ErrorIcon className={classes.icon} />
      <div>
        <AlertTitle>{title}</AlertTitle>
        <Alert severity="error">{message}</Alert>
      </div>
    </div></div>
  );
};

export default Error;
