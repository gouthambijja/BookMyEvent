import React from 'react';
import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchOrganisationById, fetchOrganisations } from '../Features/ReducerSlices/OrganisationsSlice';
const OrganisationsListPage = () => {
    const dispatch = useDispatch();
    useEffect(() => {
        //dispatch(fetchOrganisations());
        dispatch(fetchOrganisationById("C934E5F9-B6B5-4D28-932C-F96C981AE70F"));
    }, [dispatch]);
    var organisations = useSelector(store => store.organisations.organisations);
    console.log(organisations);



    return (
        <div>
            Hello world
        </div>
    );
}

export default OrganisationsListPage;
