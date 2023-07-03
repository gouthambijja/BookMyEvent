import React from 'react';
import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Card, CardContent, Typography, Grid, Button, CardActionArea, CardActions } from '@mui/material';
import { deleteOrganisation, fetchOrganisationById, fetchOrganisations } from '../Features/ReducerSlices/OrganisationsSlice';
import store from '../App/store';
import { makeStyles } from "@material-ui/core/styles";
import { useNavigate } from "react-router-dom";
import ConfirmationDialog from '../Components/ConfirmationDialog';
import { toast } from 'react-toastify';

const useStyles = makeStyles((theme) => ({
    card: {
        backgroundColor: theme.palette.background.default,
        borderRadius: theme.shape.borderRadius,
        boxShadow: theme.shadows[2],
        height: '100%',
    },
    cardContent: {
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'space-between',
        height: '100%',
    },
    organizationName: {
        fontWeight: 'bold',
        marginBottom: theme.spacing(1),
    },
    organizationLocation: {
        fontStyle: 'italic',
        color: theme.palette.text.secondary,
        marginBottom: theme.spacing(2),
    },
}));

const OrganizationCard = ({ organization }) => {
    console.log(organization);
    const classes = useStyles();
    const { organisationId } = organization;
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const [openDialog, setOpenDialog] = useState(false);
    const [block, setBlock] = useState(organization.isActive);
    const handleOrganisation = () => {
        navigate(`${organisationId}`);
    }
    const profile = useSelector((state) => state.profile.info);
    const handleBlock = () => {
        setOpenDialog(true);
    }

    const handleCancel = () => {
        setOpenDialog(false);
    };

    const handleConfirm = () => {
        dispatch(deleteOrganisation({ organisationId: organisationId, administratorId: profile.administratorId }));
        setOpenDialog(false);
        setBlock(!block);
    };

    return (
        <>
        <Card className={classes.card}>
            <CardActionArea onClick={() => handleOrganisation()}>
                <CardContent className={classes.cardContent}>
                    <div>
                        <Typography variant="h5" className={classes.organizationName} >
                            {organization.organisationName}
                        </Typography>
                        <Typography variant="body2" className={classes.organizationLocation}>
                            {organization.location}
                        </Typography>
                        <Typography variant="body2" color="text.secondary">
                            {organization.organisationDescription}
                        </Typography>
                    </div>
                </CardContent>
            </CardActionArea>
            <CardActions sx={{ display: 'flex', justifyContent: 'right' }}>
                <Button size="small" color="primary" onClick={() => handleBlock()}>
                    Block
                </Button>
            </CardActions>
            </Card>
            <ConfirmationDialog key="confirmation-dialog"
                open={openDialog}
                title="Confirmation"
                content={organization.isActive ? "Are you sure you want to block?" : "Are you sure you want to unblock?"}
                onConfirm={handleConfirm}
                onCancel={handleCancel}
            />
        </>
    );
}

const OrganisationsListPage = () => {
    const dispatch = useDispatch();
    //var organisations = store.getState().organisations.organisations;
    var organisations = useSelector((state) => state.organisations.organisations);
    console.log(organisations);



    return (<div style={{ padding: '30px' }}>
        <h1 >Organisations</h1>
        <Grid container spacing={2}>
            {organisations.map((organization) => (
                <Grid item xs={12} sm={6} md={4} key={organization.organisationId
                }>
                    <OrganizationCard organization={organization} />
                </Grid>
            ))}
        </Grid>
    </div>

    );
}



export default OrganisationsListPage;
