import React, { useState } from 'react';
import { useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import { useParams } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import store from "../App/store";
import { RotateLeftOutlined } from '@material-ui/icons';
import { Button, Card, CardActionArea, CardActions, CardContent, CardMedia, Typography } from '@mui/material';
import { fetchOrganisationOrganisers, clearMyOrganisers, blockOrganiser } from "../Features/ReducerSlices/OrganisersSlice";
import { useDispatch, useSelector } from 'react-redux';
import ConfirmationDialog from '../Components/ConfirmationDialog';
import { toast } from 'react-toastify';
import {GroupAdd,} from "@material-ui/icons";
const useStyles = makeStyles({
    root: {
        // width: '100%',
        // maxWidth: 360,
        display: 'flex',
        flexWrap: 'wrap',
        flexDirection: 'column',
        // justifyContent: 'space-between',
        alignItems: 'center',
    },
    section: {
        marginBottom: '1rem',
        display: 'flex',
        flexWrap: 'wrap',
        justifyContent: 'center',
    },
    addButton: {
        padding:'16px',
        cursor:'pointer',
        borderRadius: '4px',
        border:'0px ',
        float: 'right',
        backgroundColor: '#2986CE',
        margin:'1%',
        color:'white',
        display:'flex',
        alignItems:'center',
        fontWeight: 'bold',
        '&:hover': {
            backgroundColor: '#555',
        },
    },
    sectionTitle: {
        fontWeight: 'bold',
        marginBottom: '0.5rem',
        fontSize: '1.5rem',
        textAlign: 'center',
        color: '#333', 
    },
    card: {
        width: '250px',
        height: '300px',
        marginBottom: '0.5rem',
        marginTop: '1rem',
        padding: '15px',
        marginRight: '1rem',
        backgroundColor: '#FFF',
        borderRadius: '4px',
        boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.5)',
        transition: 'transform 0.@s ease',
        '&:hover': {
            transform: 'scale(1.01)',
        },
    },
    cardContent: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        padding: '1rem',
    },
    memberImageContainer: {
        width: '100px',
        height: '100px',
        marginBottom: '0.5rem',
        borderRadius: '50%',
        overflow: 'hidden',
    },
    memberImage: {
        width: '100%',
        height: '100%',
        objectFit: 'cover',
    },
    memberName: {
        fontWeight: 'bold',
        color: '#444',
        marginBottom: '0.5rem',
    },
    memberRole: {
        fontSize: '0.9rem',
        color: '#666',
        marginBottom: '0.5rem',
    },
    memberEmail: {
        fontSize: '0.9rem',
        color: '#666',
        marginBottom: '0.5rem',

    },

    filterButton: {
     
        margin: '0.5rem 0.5rem',
        padding: '0.5rem 1rem',
        border: 'none',
        borderRadius: '4px',
        fontWeight: 'bold',
        cursor: 'pointer',

        backgroundColor: '#3f50b5',
        color: '#fff',
        transition: 'background-color 0.3s ease',
        '&:hover': {
            backgroundColor: '#555',
        },
    },
    activeButton: {
        backgroundColor: '#d81b60',
    },
});
const OrganisationTree = () => {
    const profile = store.getState().profile.info;
    const { id } = useParams();
    const dispatch = useDispatch();
    const orgMembers = useSelector(store => store.organisers.myOrganisationOrganisers);
    const auth = useSelector(store => store.auth);
    const [selectedRole, setSelectedRole] = useState('2');
    const [selectedUser, setSeletedUser] = useState({});
    const [openDialog, setOpenDialog] = useState(false);
    const navigate= useNavigate();
    const handleRoleFilter = (roleId) => {
        setSelectedRole(roleId);
    };
    const classes = useStyles();
    const membersByRole = {};

    const handleBlock = (organiser) => {
        setSeletedUser(organiser);
        setOpenDialog(true);
        //dispatch(deleteOrganisation(organisationId));
    }

    const handleCancel = () => {
        setOpenDialog(false);
    };

    const handleConfirm = async () => {
        //dispatch();
        let temp = { id: selectedUser.administratorId, deletedById: profile.administratorId };
        await dispatch(blockOrganiser(temp));
        setOpenDialog(false);
        toast.success("Blocked Successfully");
        //setBlock(!block);
    };


    // Group members by role
    orgMembers.forEach((member) => {
        const { roleId } = member;

        if (!membersByRole[roleId]) {
            membersByRole[roleId] = [];
        }

        membersByRole[roleId].push(member);
    });
    useEffect(() => {
        const temp = async () => {
            if (auth.role == "Admin") {

                await dispatch(fetchOrganisationOrganisers(id)).unwrap();
                return null;
            }
            else {
                if (store.getState().organisers.myOrganisationOrganisers.length > 0) return null;
                else {

                    await dispatch(fetchOrganisationOrganisers(id)).unwrap();
                    return null;
                }
            }
        }
        temp();
        return (() => {
            dispatch(clearMyOrganisers());
        })

    }, []);
    return (<>

        <div className={classes.buttonContainer}>
            <button
                className={`${classes.filterButton} ${selectedRole === '2' && classes.activeButton}`}
                onClick={() => handleRoleFilter('2')}
            >
                Owner
            </button>
            <button
                className={`${classes.filterButton} ${selectedRole === '3' && classes.activeButton}`}
                onClick={() => handleRoleFilter('3')}
            >
                Secondary Owner
            </button>
            <button
                className={`${classes.filterButton} ${selectedRole === '4' && classes.activeButton}`}
                onClick={() => handleRoleFilter('4')}
            >
                Peers
            </button>
            {auth.role == "Owner" && selectedRole == '3' ? <>
                <button className={classes.addButton} onClick={()=>navigate("/organiser/addSecondaryOwner")}><GroupAdd/> <span style={{marginLeft:'5px'}}> Add Secondary Owner</span></button>
            </> : <></>}
        </div>
        <div className={classes.root}>

            <div >
                {/* <h2 className={classes.sectionTitle}> {selectedRole == 2 ? "Owner" : selectedRole == 3 ? "Secondary Owners" : "Peers"}</h2> */}
                {membersByRole[selectedRole] != null ? <>
                    <div className={classes.section}>
                        {membersByRole[selectedRole].map((member, index) => (
                            <>
                                <Card key={index} className={classes.card} sx={{ display: 'flex', flexDirection: "column", height: "auto" }}>
                                    <CardContent className={classes.cardContent} sx={{ flexBasis: "90%" }}>
                                        <Typography variant="h6" component="h2" className={classes.memberImageContainer}>
                                            <img
                                                src={`data:image/jpeg;base64,${member.imgBody}`}
                                                alt={member.administratorName}
                                                className={classes.memberImage}
                                            ></img>
                                        </Typography>
                                        <Typography variant="h6" component="h2" className={classes.memberName}>
                                            {member.administratorName}
                                        </Typography>
                                        <Typography color="textSecondary" gutterBottom className={classes.memberRole}>
                                            Address: {member.administratorAddress}
                                        </Typography>
                                        <Typography color="textSecondary" className={classes.memberEmail}>
                                            Email: {member.email}
                                        </Typography>
                                        <Typography color="textSecondary" className={classes.memberEmail}>
                                            Phone: {member.phoneNumber}
                                        </Typography>
                                    </CardContent>
                                    <CardActions sx={{ display: 'flex', justifyContent: 'right', flexBasis: "10%" }}>
                                        {profile.roleId < member.roleId && member.roleId !== 2 && <Button onClick={() => handleBlock(member)}>{member.isActive ? 'Block' : 'Unblock'}</Button>}
                                    </CardActions>
                                </Card>
                            </>
                        ))}
                    </div>
                </> : <>No Members to display</>}
            </div>
            <ConfirmationDialog key="confirmation-dialog"
                open={openDialog}
                title="Confirmation"
                content={selectedUser.isActive ? "Are you sure you want to block?" : "Are you sure you want to unblock?"}
                onConfirm={handleConfirm}
                onCancel={handleCancel}
            />
        </div>
    </>)
}
export default OrganisationTree;