import { useEffect, useState } from "react";
import store from "../App/store";
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Button, Paper, Container, Avatar } from '@mui/material';
import organiserServices from "../Services/OrganiserServices";
import OrganisationService from "../Services/OrganisationService";
import { acceptOrganiser, fetchRequestedPeers, rejectOrganiser } from "../Features/ReducerSlices/OrganisersSlice";
import { useDispatch, useSelector } from "react-redux";
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, TextField, makeStyles } from '@material-ui/core';
import { Typography } from '@material-ui/core';
import { toast } from 'react-toastify';
import ConfirmationDialog from "../Components/ConfirmationDialog";
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
    norequests: {
        color: theme.palette.error.main,
        margin: 'auto',
        textAlign: 'center',
    },
    tableContainer: {
        marginTop: theme.spacing(2),
    },
    tableCellHeader: {
        fontWeight: 'bold',
        fontSize: '2rem',
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

const PeerRequest = () => {
    const [open, setOpen] = useState(false);
    const [selectedPeer, setSelectedPeer] = useState(null);
    const [reason, setReason] = useState('');

    const profile = store.getState().profile.info;
    const requestedPeers = useSelector(store => store.organisers.requestedOrganisers);
    const dispatch = useDispatch();

    const handleDelete = (peer) => {
        setSelectedPeer(peer);
        setOpen(true);
    };

    const handleAccept = async (peer) => {
        const updatedPeer = {
            administratorId: peer.administratorId,
            administratorName: peer.administratorName,
            administratorAddress: peer.administratorAddress,
            Email: peer.email,
            PhoneNumber: peer.phoneNumber,
            AccountCredentialsId: peer.AccountCredentialsId,
            RoleId: peer.RoleId,
            isAccepted: true,
            imgBody: peer.imgBody,
            ImageName: peer.ImageName,
            AcceptedBy: profile.administratorId,
            organisationId: peer.organisationId,
            IsActive: true,
        };

        dispatch(acceptOrganiser(updatedPeer));
        toast.success('Accepted!', {
            position: toast.POSITION.BOTTOM_RIGHT
        });
    };

    const handleReject = (reason) => {
        if (reason) {
            const rejectedPeer = {
                administratorId: selectedPeer.administratorId,
                administratorName: selectedPeer.administratorName,
                administratorAddress: selectedPeer.administratorAddress,
                Email: selectedPeer.email,
                PhoneNumber: selectedPeer.phoneNumber,
                AccountCredentialsId: selectedPeer.AccountCredentialsId,
                RoleId: selectedPeer.RoleId,
                isAccepted: false,
                imgBody: selectedPeer.imgBody,
                ImageName: selectedPeer.ImageName,
                RejectedBy: profile.administratorId,
                organisationId: selectedPeer.organisationId,
                IsActive: false,
                RejectedReason: reason,
            };
            console.log(rejectedPeer);
            dispatch(rejectOrganiser(rejectedPeer));
            toast.error('Rejected!');
            setOpen(false);
        }
    };

    useEffect(() => {
        const temp = async () => {
            if (requestedPeers.length === 0) {
                dispatch(fetchRequestedPeers(profile.organisationId));
            }
        };
        temp();
    }, []);

    const classes = useStyles();

    return (
        <Container className={classes.container}>
            <Typography variant="h2" className={classes.heading}>
                Peer Requests
            </Typography>
            {requestedPeers.length !== 0 ? (
                <TableContainer component={Paper} className={classes.tableContainer}>
                    <Table>
                        <TableHead sx={{ fontSize: 2 }}>
                            <TableRow>
                                <TableCell sx={{ fontSize: 25, color: '#fff' }} className={classes.tableCellHeader}>Image</TableCell>
                                <TableCell sx={{ fontSize: 25, color: '#fff' }} className={classes.tableCellHeader}>Name</TableCell>
                                <TableCell sx={{ fontSize: 25, color: '#fff' }} className={classes.tableCellHeader}>Address</TableCell>
                                <TableCell sx={{ fontSize: 25, color: '#fff' }} className={classes.tableCellHeader}>Action</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {requestedPeers.map((peer) => (
                                <TableRow key={peer.administratorId} className={classes.tableRow}>
                                    <TableCell> <img
                                        src={`data:image/jpeg;base64,${peer.imgBody}`}
                                        className={classes.avatar}

                                    ></img></TableCell>
                                    <TableCell sx={{ fontSize: 20 }}>{peer.administratorName}</TableCell>
                                    <TableCell sx={{ fontSize: 20 }}>{peer.administratorAddress}</TableCell>
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
                                                onClick={() => handleDelete(peer)}
                                                className="reject"
                                            >
                                                Reject
                                            </Button>
                                        </div>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            ) : (
                <Typography variant="h3" className={classes.norequests}>
                    No New Peer Requests
                </Typography>
            )}

            <ConfirmationDialog
                open={open}
                title="Confirm Reject"
                content="Are you sure you want to reject?"
                onConfirm={handleReject}
                onCancel={() => setOpen(false)}
                showReason={true}
            />
        </Container>
    );
};

export default PeerRequest;

