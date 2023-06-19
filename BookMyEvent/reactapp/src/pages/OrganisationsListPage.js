//import React from 'react';
import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Card, CardContent, Typography, Grid } from '@mui/material';
import { fetchOrganisationById, fetchOrganisations } from '../Features/ReducerSlices/OrganisationsSlice';
import t from "../Services/OrganisationService";
import * as React from 'react';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import Snackbar from '@mui/material/Snackbar';
import MuiAlert from '@mui/material/Alert';
const OrganisationsListPage = () => {
    var open = false;
    const dispatch = useDispatch();
    const Alert = React.forwardRef(function Alert(props, ref) {
        return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
    });
    useEffect(() => {
        //dispatch(fetchOrganisations());
        dispatch(fetchOrganisationById("C934E5F9-B6B5-4D28-932C-F96C981AE70F"));
    }, [dispatch]);
    var organisations = useSelector(store => store.organisations.organisations);
    console.log(organisations);



    return (
        <div>
            Hello world
            <Snackbar open={open} autoHideDuration={500}>
                <Alert severity="success" sx={{ width: '100%' }}>
                    This is a success message!
                </Alert>
            </Snackbar>
        </div>
    );
}

export default OrganisationsListPage;
