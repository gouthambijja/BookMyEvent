import { useEffect, useState } from "react";
import store from "../App/store";
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Button, Paper, Container, Avatar } from '@mui/material';
import organiserServices from "../Services/OrganiserServices";
import OrganisationService from "../Services/OrganisationService";
import { acceptOrganiser, fetchRequestedOwners, rejectOrganiser } from "../Features/ReducerSlices/OrganisersSlice";
import { useDispatch, useSelector } from "react-redux";
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, TextField, makeStyles } from '@material-ui/core';
import { Typography } from '@material-ui/core';
import { toast } from 'react-toastify';

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

const OwnerRequests = () => {
    const [open, setOpen] = useState(false);
    const [selectedOwner, setSelectedOwner] = useState(null);
    const [reason, setReason] = useState('');

    const profile = store.getState().profile.info;
    const requestedOwners = useSelector(store => store.organisers.requestedOrganisers);
    const dispatch = useDispatch();

    const handleDelete = (owner) => {
        setSelectedOwner(owner);
        setOpen(true);
    };

    const handleAccept = async (owner) => {
        const updatedOwner = {
            administratorId: owner.administratorId,
            administratorName: owner.administratorName,
            administratorAddress: owner.administratorAddress,
            Email: owner.email,
            PhoneNumber: owner.phoneNumber,
            AccountCredentialsId: owner.accountCredentialsId,
            RoleId: owner.roleId,
            isAccepted: true,
            imgBody: owner.imgBody,
            ImageName: owner.imageName,
            AcceptedBy: profile.administratorId,
            organisationId: owner.organisationId,
            IsActive: true,
        };

        dispatch(acceptOrganiser(updatedOwner));
        toast.success('Accepted!');
    };

    const handleReject = (reason) => {
        if (reason) {
            const rejectedOwner = {
                administratorId: selectedOwner.administratorId,
                administratorName: selectedOwner.administratorName,
                administratorAddress: selectedOwner.administratorAddress,
                Email: selectedOwner.email,
                PhoneNumber: selectedOwner.phoneNumber,
                AccountCredentialsId: selectedOwner.accountCredentialsId,
                RoleId: selectedOwner.roleId,
                isAccepted: false,
                imgBody: selectedOwner.imgBody,
                ImageName: selectedOwner.imageName,
                RejectedBy: profile.administratorId,
                organisationId: selectedOwner.organisationId,
                IsActive: false,
                RejectedReason: reason,
            };

            dispatch(rejectOrganiser(rejectedOwner));
            toast.error('Rejected!');
            setOpen(false);
        }
    };

    useEffect(() => {
        const temp = async () => {
            if (requestedOwners.length === 0) {
                dispatch(fetchRequestedOwners());
            }
        };
        temp();
    }, []);

    const classes = useStyles();

    return (
        <Container className={classes.container}>
            <Typography variant="h2" className={classes.heading}>
                Owner Requests
            </Typography>
            {requestedOwners.length !== 0 ? (
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
                            {requestedOwners.map((owner) => (
                                <TableRow key={owner.administratorId} className={classes.tableRow}>
                                    <TableCell> <img
                                        src={`data:image/jpeg;base64,${owner.imgBody}`}
                                        className={classes.avatar}
                                    /></TableCell>
                                    <TableCell sx={{ fontSize: 20 }}>{owner.administratorName}</TableCell>
                                    <TableCell sx={{ fontSize: 20 }}>{owner.administratorAddress}</TableCell>
                                    <TableCell>
                                        <div className={classes.actionButtons}>
                                            <Button
                                                variant="outlined"
                                                onClick={() => handleAccept(owner)}
                                                className="accept"
                                            >
                                                Accept
                                            </Button>
                                            <Button
                                                variant="outlined"
                                                onClick={() => handleDelete(owner)}
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
                    No New Owner Requests
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

function ConfirmationDialog({ open, title, content, onConfirm, onCancel, showReason }) {
    const [reason, setReason] = useState('');

    const handleConfirm = () => {
        onConfirm(reason);
    };

    const handleCancel = () => {
        onCancel();
    };

    const handleChangeReason = (event) => {
        setReason(event.target.value);
    };

    return (
        <Dialog open={open} onClose={handleCancel}>
            <DialogTitle>{title}</DialogTitle>
            <DialogContent>
                <DialogContentText>{content}</DialogContentText>
                {showReason && (
                    <TextField
                        autoFocus
                        margin="dense"
                        label="Reason for Rejection"
                        type="text"
                        fullWidth
                        value={reason}
                        onChange={handleChangeReason}
                    />
                )}
            </DialogContent>
            <DialogActions>
                <Button onClick={handleCancel} color="primary">
                    Cancel
                </Button>
                <Button onClick={handleConfirm} color="secondary" autoFocus>
                    Confirm
                </Button>
            </DialogActions>
        </Dialog>
    );
}

export default OwnerRequests;
