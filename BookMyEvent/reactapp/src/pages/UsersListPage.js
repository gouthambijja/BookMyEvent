import { Button, Grid, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TextField, InputLabel, MenuItem, FormControl, Select} from '@mui/material';
import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import UserServices from '../Services/UserServices';
import ConfirmationDialog from '../Components/ConfirmationDialog';

const UserListPage = () => {
    const [openDialog, setOpenDialog] = useState(false);
    const [users, setUsers] = useState([]);
    const [selectedUser, setSeletedUser] = useState({});
    const [filters, setFilters] = useState({
        name: '',
        email: '',
        phoneNumber: '',
        isActive: true,
    });
    var count = 0;
    const profile = useSelector((state) => state.profile.info);
    useEffect(() => {
    }, [users]);

    const fetchUsers = async () => {
        setUsers(await UserServices().filteredUsers(filters));
    };

    const handleFilterChange = event => {
        setFilters({ ...filters, [event.target.name]: event.target.value });
    };

    const handleSearch = () => {
        fetchUsers();
    };

    const handleCancel = () => {
        setOpenDialog(false);
    };

    const handleConfirm = async () => {
        //dispatch(deleteOrganisation({ organisationId: organisationId, administratorId: profile.administratorId }));
        setOpenDialog(false);
        var updatedUser = await UserServices().blockUser(selectedUser.userId, profile.administratorId);
        var updatedUsersList = users.filter(x => x.userId !== updatedUser.userId);
        setUsers(updatedUsersList);
    };

    const handleBlockUnblock = async (user) => {
        setOpenDialog(true);
        setSeletedUser(user);
        //var updatedUser = await UserServices().blockUser(userId, blockedBy);
        //var updatedUsersList = users.filter(x => x.userId !== updatedUser.userId);
        //setUsers(updatedUsersList);

    };

    return (
        <div style={{ padding: '40px' }}>
            <Grid container spacing={2}>
                <Grid item xs={12} sm={6} md={4}>
                    <TextField
                        fullWidth
                        label="Name"
                        name="name"
                        value={filters.name}
                        onChange={handleFilterChange}
                    />
                </Grid>
                <Grid item xs={12} sm={6} md={4}>
                    <TextField
                        fullWidth
                        label="Email"
                        name="email"
                        value={filters.email}
                        onChange={handleFilterChange}
                    />
                </Grid>
                <Grid item xs={12} sm={6} md={4}>
                    <TextField
                        fullWidth
                        label="Phone Number"
                        name="phoneNumber"
                        value={filters.phoneNumber}
                        onChange={handleFilterChange}
                    />
                </Grid>
                <Grid item xs={12} sm={6} md={4}>
                    <FormControl fullWidth>
                        <InputLabel id="Status">Status</InputLabel>
                        <Select
                            labelId="isActive"
                            id="isActive"
                            name = "isActive"
                            value={filters.isActive}
                            label="Status"
                            onChange={handleFilterChange}
                        >
                            <MenuItem value={true}>Active</MenuItem>
                            <MenuItem value={false}>Blocked</MenuItem>
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item xs={12} sm={6} md={4}>
                    <Button
                        fullWidth
                        variant="contained"
                        color="primary"
                        onClick={handleSearch}
                    >
                        Search
                    </Button>
                </Grid>
            </Grid>

            {users.length === 0 ? (
                <div>No users found</div>
            ) : (
                <TableContainer>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Name</TableCell>
                                <TableCell>Email</TableCell>
                                <TableCell>Phone Number</TableCell>
                                <TableCell>Status</TableCell>
                                <TableCell>Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {users.map(user => (
                                <TableRow key={user.userId}>
                                    <TableCell>{user.name}</TableCell>
                                    <TableCell>{user.email}</TableCell>
                                    <TableCell>{user.phoneNumber}</TableCell>
                                    <TableCell>{user.isActive ? 'Active' : 'Blocked'}</TableCell>
                                    <TableCell>
                                        <Button
                                            variant="outlined"
                                            color={user.isActive ? 'error' : 'primary'}
                                            onClick={() => handleBlockUnblock(user)}
                                        >
                                            {user.isActive ? 'Block' : 'Unblock'}
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
            <ConfirmationDialog key="confirmation-dialog"
                open={openDialog}
                title="Confirmation"
                content={selectedUser.isActive ? "Are you sure you want to block?" : "Are you sure you want to unblock?"}
                onConfirm={handleConfirm}
                onCancel={handleCancel}
            />
        </div>
    );
};

export default UserListPage;
