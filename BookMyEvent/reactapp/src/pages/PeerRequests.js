import { useEffect, useState } from "react";
import store from "../App/store";
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Button,Paper, Container,Avatar } from '@mui/material';
import organiserServices from "../Services/OrganiserServices";
import OrganisationService from "../Services/OrganisationService";
import { acceptOrganiser, fetchRequestedPeers, rejectOrganiser } from "../Features/ReducerSlices/OrganisersSlice";
import { useDispatch, useSelector } from "react-redux";
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from '@material-ui/core';
import { Typography, makeStyles } from '@material-ui/core';


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
const PeerRequest=()=>{
  const [open, setOpen] = useState(false);
    const profile = store.getState().profile.info;
    const requestedPeers=useSelector(store=>store.organisers.requestedOrganisers);
    const dispatch=useDispatch();
    const handleDelete = () => {
      setOpen(true);
    };
    const handleClose = () => {
      setOpen(false);
    };
      const handleAccept = async(peer) => {
        console.log(peer);
        const updatedPeer={
          administratorId:peer.administratorId,
          administratorName:peer.administratorName,
          administratorAddress:peer.administratorAddress,
          Email:peer.email,
          PhoneNumber:peer.phoneNumber,
          AccountCredentialsId:peer.AccountCredentialsId,
          RoleId:peer.RoleId,
          isAccepted:true,
          imgBody:peer.imgBody,
          ImageName:peer.ImageName,
          AcceptedBy:profile.administratorId,
          organisationId:peer.organisationId,
          IsActive:true,
        }      
         dispatch(acceptOrganiser(updatedPeer))        
      };
      const handleReject = (peer) => {
      
        const rejectedPeer={
          administratorId:peer.administratorId,
          administratorName:peer.administratorName,
          administratorAddress:peer.administratorAddress,
          Email:peer.email,
          PhoneNumber:peer.phoneNumber,
          AccountCredentialsId:peer.AccountCredentialsId,
          RoleId:peer.RoleId,
          isAccepted:false,
          imgBody:peer.imgBody,
          ImageName:peer.ImageName,
          RejectedBy:profile.administratorId,
          organisationId:peer.organisationId,
          IsActive:false,
        }
        dispatch(rejectOrganiser(rejectedPeer));
        setOpen(false);
      
      };
    useEffect(()=>{
        const temp=async()=>{
          if(requestedPeers.length===0){
            dispatch(fetchRequestedPeers(profile.organisationId))           
          }
        }
        temp();
    },[])
    const classes = useStyles();
    return(<>
    <Container className={classes.container}>
      <Typography variant="h2" className={classes.heading} >
        Peer Requests
      </Typography>
    {requestedPeers.length!==0?<>
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
          {requestedPeers.map((peer) => (
            <TableRow key={peer.administratorId} className={classes.tableRow}>
              <TableCell> <img
            src={`data:image/jpeg;base64,${peer.imgBody}`}
            className={classes.avatar}
            
          ></img></TableCell>
              <TableCell sx={{ fontSize:20}}>{peer.administratorName}</TableCell>
              <TableCell sx={{ fontSize:20}}>{peer.administratorAddress}</TableCell>
              <TableCell>
                <div className={classes.actionButtons}>
              <Button
                  variant="outlined"
                  onClick={() => handleAccept(peer)}
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
                <DeleteConfirmationDialog  open={open} onClose={handleClose} onConfirm={()=>handleReject(peer)} />
                </div>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
    </>:<>No new Peer Requests</>}
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
export default PeerRequest;