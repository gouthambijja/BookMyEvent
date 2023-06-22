import { useEffect, useState } from "react";
import store from "../App/store";
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Button,Paper, Container,Avatar } from '@mui/material';
import organiserServices from "../Services/OrganiserServices";
import OrganisationService from "../Services/OrganisationService";
import { acceptOrganiser, fetchRequestedOwners,  rejectOrganiser } from "../Features/ReducerSlices/OrganisersSlice";
import { useDispatch, useSelector } from "react-redux";
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from '@material-ui/core';
import { Typography, makeStyles } from '@material-ui/core';
import { fetchOrganisations } from "../Features/ReducerSlices/OrganisationsSlice";


const useStyles = makeStyles((theme) => ({
  container: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    height: '100vh',
  },
  heading: {
    fontSize: '2.5rem',
    fontWeight: 'bold',
    color: theme.palette.primary.main,
    textAlign: 'center',
    marginBottom: theme.spacing(2),
  },
  norequests:{
    color:theme.palette.error.main,
    margin:'auto',
    textAlign: 'center',
  },
  tableContainer: {
    marginTop: theme.spacing(2),
  },
  tableCellHeader: {
    fontWeight: 'bold',
    fontSize:'2rem',
    backgroundColor: theme.palette.primary.main, 
  },
  tableRow: {
    '&:hover': {
      backgroundColor: theme.palette.action.hover,
    },
  },
  avatar: {
    width: theme.spacing(8),
    height: theme.spacing(8),
  },
  actionButtons: {
    '& > *': {
      marginRight: theme.spacing(1),
    },
    '& button': {
      textTransform: 'capitalize',
      marginRight: theme.spacing(5)
    },
    '& button.reject': {
      color: theme.palette.error.main,
      borderColor: theme.palette.error.main,
    },
    '& button.accept': {
      color: theme.palette.success.main,
      borderColor: theme.palette.success.main,
    },
  },
}));
const OwnerRequests=()=>{
  const [open, setOpen] = useState(false);
    const profile = store.getState().profile.info;
    const requestedOwners=useSelector(store=>store.organisers.requestedOrganisers);
    const dispatch=useDispatch();
    const handleDelete = () => {
      setOpen(true);
    };
    const handleClose = () => {
      setOpen(false);
    };
      const handleAccept = async(Owner) => {
        console.log(Owner);
        const updatedOwner={
          administratorId:Owner.administratorId,
          administratorName:Owner.administratorName,
          administratorAddress:Owner.administratorAddress,
          Email:Owner.email,
          PhoneNumber:Owner.phoneNumber,
          AccountCredentialsId:Owner.accountCredentialsId,
          RoleId:Owner.roleId,
          isAccepted:true,
          imgBody:Owner.imgBody,
          ImageName:Owner.imageName,
          AcceptedBy:profile.administratorId,
          organisationId:Owner.organisationId,
          IsActive:true,
        }      
         dispatch(acceptOrganiser(updatedOwner))  
        //  let pageNumber=1; 
        // await store.dispatch(fetchOrganisations({pageNumber:pageNumber,pageSize:10})).unwrap();

      };
      const handleReject = (Owner) => {
      
        const rejectedOwner={
          administratorId:Owner.administratorId,
          administratorName:Owner.administratorName,
          administratorAddress:Owner.administratorAddress,
          Email:Owner.email,
          PhoneNumber:Owner.phoneNumber,
          AccountCredentialsId:Owner.accountCredentialsId,
          RoleId:Owner.roleId,
          isAccepted:false,
          imgBody:Owner.imgBody,
          ImageName:Owner.imageName,
          RejectedBy:profile.administratorId,
          organisationId:Owner.organisationId,
          IsActive:false,
        }
        dispatch(rejectOrganiser(rejectedOwner));
        setOpen(false);
      
      };
    useEffect(()=>{
        const temp=async()=>{
          if(requestedOwners.length===0){
            dispatch(fetchRequestedOwners())           
          }
        }
        temp();
    },[])
    const classes = useStyles();
    return(<>
    <Container className={classes.container}>
      <Typography variant="h2" className={classes.heading} >
        Owner Requests
      </Typography>
    {requestedOwners.length!==0?<>
    <TableContainer component={Paper} className={classes.tableContainer} >
      <Table >
        <TableHead sx={{ fontSize:2}}>
          <TableRow >
            <TableCell sx={{ fontSize:25,color:'#fff'}} className={classes.tableCellHeader}>Image</TableCell>
            <TableCell sx={{ fontSize:25,color:'#fff'}} className={classes.tableCellHeader}>Name</TableCell>
            <TableCell sx={{ fontSize:25,color:'#fff'}} className={classes.tableCellHeader}>Address</TableCell>
            <TableCell sx={{ fontSize:25,color:'#fff'}} className={classes.tableCellHeader}>Action</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {requestedOwners.map((Owner) => (
            <TableRow key={Owner.administratorId} className={classes.tableRow}>
              <TableCell> <img
            src={`data:image/jpeg;base64,${Owner.imgBody}`}
            className={classes.avatar}
            
          ></img></TableCell>
              <TableCell sx={{ fontSize:20}}>{Owner.administratorName}</TableCell>
              <TableCell sx={{ fontSize:20}}>{Owner.administratorAddress}</TableCell>
              <TableCell>
                <div className={classes.actionButtons}>
              <Button
                  variant="outlined"
                  onClick={() => handleAccept(Owner)}
                  className="accept"
                >
                  Accept
                </Button>
                <Button
                  variant="outlined"
                  onClick={handleDelete}
                  className="reject"
                >
                  Reject
                </Button>
                <DeleteConfirmationDialog  open={open} onClose={handleClose} onConfirm={()=>handleReject(Owner)} />
                </div>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
    </>:<><Typography variant="h3" className={classes.norequests} >
       No New Owner Requests
      </Typography></>}
    </Container>
    </>)

}

function DeleteConfirmationDialog({ open, onClose, onConfirm }) {
  const handleConfirm = () => {
    onConfirm();
    onClose();
  };

  const handleCancel = () => {
    onClose();
  };

  return (
    <Dialog open={open} onClose={handleCancel}>
      <DialogTitle>Confirm Delete</DialogTitle>
      <DialogContent>
        <DialogContentText>Are you sure you want to delete?</DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleCancel} color="primary">
          No
        </Button>
        <Button onClick={handleConfirm} color="secondary" autoFocus>
          Yes
        </Button>
      </DialogActions>
    </Dialog>
  );
}
export default OwnerRequests;