import React, { useEffect, useRef, useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import store from "../App/store";
import { Button, Popover, Typography, createTheme, ThemeProvider } from '@material-ui/core';
import {
    Drawer,
    IconButton,
    List,
    ListItem,
    ListItemIcon,
    ListItemText,
} from "@material-ui/core";
import {
    Menu as MenuIcon,
    ChevronLeft as ChevronLeftIcon,
    Inbox as InboxIcon,
    ExitToApp,
    GroupAdd,
    AccountCircle,
    Home,
    Event,
    EventAvailable,
    EventBusy,
    EventNote,
    GroupWork,
    Category,
    PostAdd,
    PersonAdd,
    ArrowForwardIosTwoTone,
    ArrowBackIos,
    SpeakerGroup,
    AccountTree,
    People,
  
} from "@material-ui/icons";
import ListItemSecondaryAction from '@mui/material/ListItemSecondaryAction';
import Popper from '@mui/material/Popper';
import ListItemButton from '@mui/material/ListItemButton';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import Paper from '@mui/material/Paper';
import Badge from '@mui/material/Badge';
import { useNavigate,useLocation } from "react-router-dom";
import useLogout from "../Hooks/Logout";
import { useSelector } from "react-redux";

const drawerWidth = 240;
const theme = createTheme({
    components: {
        MuiPaper: {
            styleOverrides: {
                elevation8: {
                    boxShadow: 'none',
                },
                rounded: {
                    borderRadius: 0,
                },
            },
        },
    },
});
const useStyles = makeStyles((theme) => ({
    root: {
        display: "flex",
    },
    customPaper: {
        boxShadow: 'none',
        borderRadius: 'none'
    },
    appBar: {
        transition: theme.transitions.create(["margin", "width"], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen,
        }),
    },
    activeBar: {
        '&:hover': {
            backgroundColor: theme.palette.primary.main,
            color: "white",
        },
        backgroundColor: theme.palette.primary.main,
        color: "white",
    },

    appBarShift: {
        width: `calc(100% - ${drawerWidth}px)`,
        marginLeft: drawerWidth,
        transition: theme.transitions.create(["margin", "width"], {
            easing: theme.transitions.easing.easeOut,
            duration: theme.transitions.duration.enteringScreen,
        }),
    },
    menuButton: {
        marginRight: theme.spacing(2),
    },
    hide: {
        display: "none",
    },
    drawer: {
        width: drawerWidth,
        flexShrink: 0,
    },
    drawerPaper: {
        width: drawerWidth,
        background: "#f5f5f5",
        paddingBottom: '64px',
        backdropFilter: "blur(6px)",
    },
    drawerHeader: {
        display: "flex",
        alignItems: "center",
        padding: theme.spacing(0, 1),
        ...theme.mixins.toolbar,
        justifyContent: "flex-end",
    },
    content: {
        flexGrow: 1,
        padding: theme.spacing(3),
        transition: theme.transitions.create("margin", {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen,
        }),
        marginLeft: 0,
    },
}));

function Sidebar({ open, setOpen }) {
    const navigate = useNavigate();
    const location =useLocation();



    const auth = useSelector((store) => store.auth);
    const NoOfRequestedPeers = useSelector(store => store.organisers.NoOfRequestedOrganisers);
    let NoOfEventRequests = useSelector((state) => state.events.noOfEventRequests);

    const Profile = useSelector((state) => (state.profile.info));
    const logout = useLogout();
    const classes = useStyles();


    const handleDrawerOpen = () => {
        setOpen(true);
    };

    const handleDrawerClose = () => {
        setOpen(false);
    };
    const handleLogout = async () => {
        if (await logout()) {
            navigate("/");
            window.location.reload();
            handleDrawerClose();
        }
    };
    const handleProfile = async () => {
        if (auth.role == "Admin") {
            handleDrawerClose();
            navigate("/admin/profile");
        } else if (
            auth.role == "Owner" ||
            auth.role == "Peer" ||
            auth.role == "Secondary_Owner"
        ) {
            handleDrawerClose();
            navigate("/organiser/profile");
        } else {
            handleDrawerClose();
            navigate("/profile");
        }
       
    };
    const handleAdd = async () => {
        handleDrawerClose();
        if (auth?.role == "Admin") navigate("/admin/addadmin");
        else navigate("/organiser/addSecondaryOwner");
    
    };
    const handleRequests = async () => {
        handleDrawerClose();
        if (auth?.role == "Admin") navigate("/admin/Requests");

    };
    const handleOrganisations = async () => {
        handleDrawerClose();
        if (auth?.role == "Admin") navigate("/admin/Organisations");
       
    };
    const handlePeerRequests = async () => {
        handleDrawerClose();
        if (auth.role == "Owner" || auth.role == "Secondary_Owner")
            navigate("organiser/PeerRequests");

        
    };
    const handleOrganiseEvent = async () => {
        handleDrawerClose();
        navigate("/organiser/AddEvent");
       
    };
    const handleOrganisationTree = async () => {
        const profile = store.getState().profile.info;
        handleDrawerClose();
        navigate(`/organiser/OrganisationTree/${profile.organisationId}`);
        
    };
    const handleLogin = async () => {
        handleDrawerClose();
        navigate("login");
       
    };
    const handleGlobalHome = () => {
        handleDrawerClose();
        navigate("/");
        
    };
    const handleOrganiseAnEvent = async () => {
        handleDrawerClose();
        navigate("/organiser");
     

    };

    const handleMyPastEvents = async () => {
        handleDrawerClose();
        navigate("/organiser/myPastEvents");
     
    };
    const handleOrgPastEvents = async () => {
        handleDrawerClose();
        navigate("/organiser/organisationPastEvents");
       
    };

    const handleEventRequests = async () => {
        handleDrawerClose();
        navigate("/organiser/eventRequests");
      
    };

    const handleOrganisationEvents = async () => {
        handleDrawerClose();
        navigate("/organiser/organisationEvents");
      
    };
    const handleRegisteredEvents = async () => {
        handleDrawerClose();
        navigate("/registeredEvents");
       
    };
    const handleHome = async () => {
        handleDrawerClose();
        if (auth.role == "User") {
            navigate("/");
        } else if (["Owner", "Secondary_Owner", "Peer"].includes(auth.role)) {
            navigate("/organiser");
        } else {
            navigate("/Admin");
        }
      
    };
    const handleCategories = async () => {
        handleDrawerClose();
        navigate("/Admin/categories")
    }

    const handleUsersList = async () => {
        handleDrawerClose();
        navigate("/Admin/userslist")
    }

    const [isHovered, setIsHovered] = useState(false);
    const [dropdownOpen, setDropdownOpen] = useState(false);
    const anchorRef = React.useRef(null);
    const dropdownRef = React.useRef(null);


    const [anchorEl, setAnchorEl] = useState(null);

    const handleItemClick = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };
    const isDropdownOpen = Boolean(anchorEl);
    const dropdownId = isDropdownOpen ? 'dropdown-menu' : undefined;

    return (

        <div className={classes.root}>
            {open ? <div style={{ width: '100vw', height: '100vh', position: 'absolute', background: 'rgba(0,0,0,0.1)', zIndex: '40', }} onClick={handleDrawerClose}></div> : <></>}
            <Drawer
                className={classes.drawer}
                variant="persistent"
                anchor="left"
                open={open}
                classes={{
                    paper: classes.drawerPaper,
                }}
            >
                <div className={classes.drawerHeader}>
                    <IconButton onClick={handleDrawerClose}>
                        <ChevronLeftIcon />
                    </IconButton>
                </div>
                <List style={{paddingTop:'0px'}}>
                    {auth.accessToken ? (
                        <>
                            <ListItem button className={location.pathname.toLowerCase() == '/' || location.pathname.toLowerCase() == '/organiser' || location.pathname.toLowerCase() == '/admin'? classes.activeBar : ""} onClick={handleHome}>
                                <ListItemIcon>

                                    <Home className={location.pathname.toLowerCase() == '/' || location.pathname.toLowerCase() == '/organiser' || location.pathname.toLowerCase() == '/admin' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary="Home" />
                            </ListItem>
                            <ListItem button className={location.pathname.toLowerCase() == '/profile' || location.pathname.toLowerCase() == '/organiser/profile' || location.pathname.toLowerCase() == '/admin/profile' ? classes.activeBar : ""} onClick={handleProfile}>
                                <ListItemIcon >
                                    <AccountCircle className={location.pathname.toLowerCase() == '/profile' || location.pathname.toLowerCase() == '/organiser/profile' || location.pathname.toLowerCase() == '/admin/profile' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary="Profile" />
                            </ListItem>
                        </>
                    ) : (
                        <></>
                    )}
                    {auth.accessToken == "" ? (
                        <ListItem button className={location.pathname.toLowerCase() == '/' || location.pathname.toLowerCase() == '/event/:id'  ? classes.activeBar : ""} onClick={handleGlobalHome}>
                            <ListItemIcon>
                                <Home className={location.pathname.toLowerCase() == '/' ? classes.activeBar : ""} />
                            </ListItemIcon>
                            <ListItemText primary="Home" />
                        </ListItem>
                    ) : (
                        <></>
                    )}
                    {auth.accessToken == "" ? (
                        <ListItem button className={location.pathname.toLowerCase() == '/login' || location.pathname.toLowerCase() == '/register'  ? classes.activeBar : ""} onClick={handleLogin}>
                            <ListItemIcon>
                                <AccountCircle className={location.pathname.toLowerCase() == '/login' || location.pathname.toLowerCase() == '/register' ? classes.activeBar : ""} />
                            </ListItemIcon>
                            <ListItemText primary="Login" />
                        </ListItem>
                    ) : (
                        <></>
                    )}
                    {auth.accessToken == "" ? (
                        <ListItem button className={location.pathname.toLowerCase() == '/organiser/login' || location.pathname.toLowerCase() == '/organiser/register'? classes.activeBar : ""} onClick={handleOrganiseAnEvent}>
                            <ListItemIcon>
                                <PostAdd className={location.pathname.toLowerCase() == '/organiser/login' || location.pathname.toLowerCase() == '/organiser/register' ? classes.activeBar : ""} />
                            </ListItemIcon>
                            <ListItemText primary="Organise an Event?" />
                        </ListItem>
                    ) : (
                        <></>
                    )}

                    {auth.role == "User" ? (
                        <ListItem button className={location.pathname.toLowerCase() == '/registeredevents' ? classes.activeBar : ""} onClick={handleRegisteredEvents}>
                            <ListItemIcon>
                                <EventNote className={location.pathname.toLowerCase() == '/registeredevents' ? classes.activeBar : ""} />
                            </ListItemIcon>
                            <ListItemText primary="Registered Events" />
                        </ListItem>
                    ) : (
                        <></>
                    )}
                    {auth.role == "Owner" && Profile.isAccepted == true || auth.role == "Secondary_Owner" ? (
                        <ListItem button className={location.pathname.toLowerCase() == '/organiser/peerrequests' ? classes.activeBar : ""} onClick={handlePeerRequests}>
                            <ListItemIcon>
                                {NoOfRequestedPeers == 0 ?
                                    <GroupAdd className={location.pathname.toLowerCase() == '/organiser/peerrequests' ? classes.activeBar : ""} />
                                    : <Badge badgeContent={NoOfRequestedPeers} color="secondary">
                                        <GroupAdd className={location.pathname.toLowerCase() == '/organiser/peerrequests' ? classes.activeBar : ""} />
                                    </Badge>}
                            </ListItemIcon>
                            <ListItemText primary="Peer Requests" />
                        </ListItem>
                    ) : (
                        <></>
                    )}
                    {auth.role === "Admin" ? (
                        <ListItem button className={location.pathname.toLowerCase() == '/admin/addadmin'? classes.activeBar : ""} onClick={handleAdd}>
                            <ListItemIcon>
                                <GroupAdd className={location.pathname.toLowerCase() == '/admin/addadmin' ? classes.activeBar : ""} />
                            </ListItemIcon>
                            <ListItemText primary="Add Admin" />
                        </ListItem>
                    ) : (
                        <></>
                    )}
                    {auth.role === "Admin" ? (
                        <>
                            <ListItem button className={location.pathname.toLowerCase() == '/admin/organisations' ? classes.activeBar : ""} onClick={handleOrganisations}>
                                <ListItemIcon>
                                    <AccountTree className={location.pathname.toLowerCase() == '/admin/organisations' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary="Organisations" />
                            </ListItem>
                            <ListItem button className={location.pathname.toLowerCase() == '/admin/categories' ? classes.activeBar : ""} onClick={handleCategories}>
                                <ListItemIcon>
                                    <Category className={location.pathname.toLowerCase() == '/admin/categories' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary="Categories" />
                            </ListItem>
                            <ListItem button className={location.pathname.toLowerCase() == '/admin/userslist' ? classes.activeBar : ""} onClick={handleUsersList}>
                                <ListItemIcon>
                                    <People className={location.pathname.toLowerCase() == '/admin/userslist' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary="Users List" />
                            </ListItem>
                        </>
                    ) : (
                        <></>
                    )}
                    {auth.role == "Owner" && Profile.isAccepted == true ||
                        (auth.role == "Peer" && Profile.isAccepted == true) ||
                        auth.role == "Secondary_Owner" ? (
                        <ListItem button className={location.pathname.toLowerCase() == '/organiser/addevent' || location.pathname.toLowerCase() == '/organiser/createneweventregistrationform'? classes.activeBar : ""} onClick={handleOrganiseEvent}>
                            <ListItemIcon>
                                <PostAdd className={location.pathname.toLowerCase() == '/organiser/addevent' || location.pathname.toLowerCase() == '/organiser/createneweventregistrationform' ? classes.activeBar : ""} />
                            </ListItemIcon>
                            <ListItemText primary="Organise Event" />
                        </ListItem>
                    ) : (
                        <></>
                    )}
                   
                    {auth.role == "Owner" && Profile.isAccepted == true ||
                        (auth.role == "Peer" && Profile.isAccepted == true) ||
                        auth.role == "Secondary_Owner" ? (
                        <>
                            <ListItem button className={location.pathname.toLowerCase() == `/organiser/organisationtree/${Profile.organisationId}` || location.pathname.toLowerCase() == '/organiser/addsecondaryowner' ? classes.activeBar : ""} onClick={handleOrganisationTree}>
                                <ListItemIcon>

                                    <AccountTree className={location.pathname.toLowerCase() == `/organiser/organisationtree/${Profile.organisationId}` || location.pathname.toLowerCase() == '/organiser/addsecondaryowner' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary="Organisation Tree" />
                            </ListItem>

                            <ListItem
                                button
                                className={location.pathname.toLowerCase() == '/organiser/organisationevents' || location.pathname.toLowerCase() == '/organiser/mypastevents' || location.pathname.toLowerCase() == '/organiser/organisationpastevents' ? classes.activeBar : ""}

                                onClick={handleItemClick}

                            >
                                <ListItemIcon className={location.pathname.toLowerCase() == '/organiser/organisationevents' || location.pathname.toLowerCase() == '/organiser/mypastevents' || location.pathname.toLowerCase() == '/organiser/organisationpastevents' ? classes.activeBar : ""} >
                                    <AccountTreeIcon className="" />
                                </ListItemIcon>
                                <ListItemText primary="Events" />
                               
                                {anchorEl == null ? <><ArrowForwardIosTwoTone onClick={handleItemClick} style={{ fontSize: '1rem' }} /></> : <><ArrowBackIos style={{ fontSize: '1rem' }} /></>}

                             
                            </ListItem>
                            <ThemeProvider theme={theme}>
                            <Popover
                                
                                id={dropdownId}
                                open={isDropdownOpen}
                                anchorEl={anchorEl}
                                PaperProps={{
                                    style: {
                                      boxShadow: 'none', // Remove the box shadow
                                      background: "#f0f0f0",
                                    },
                                  }}
                                onClose={handleClose}
                                anchorOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                transformOrigin={{
                                    vertical: 'top',
                                    horizontal: 'left',
                                }}
                            >
                               
                                <div   onClick={handleClose}>
                                    <List>
                                        <ListItem button className={location.pathname.toLowerCase() == '/organiser/organisationevents' ? classes.activeBar : ""} onClick={handleOrganisationEvents}>
                                            <ListItemIcon>
                                                <EventAvailable className={location.pathname.toLowerCase() == '/organiser/organisationevents' ? classes.activeBar : ""} />
                                            </ListItemIcon>
                                            <ListItemText primary="Active Organisation Events" />
                                        </ListItem>
                                        <ListItem button className={location.pathname.toLowerCase() == '/organiser/mypastevents' ? classes.activeBar : ""} onClick={handleMyPastEvents}>
                                            <ListItemIcon>
                                                <Event className={location.pathname.toLowerCase() == '/organiser/mypastevents' ? classes.activeBar : ""} />
                                            </ListItemIcon>
                                            <ListItemText primary="My Past events" />
                                        </ListItem>
                                        <ListItem button className={location.pathname.toLowerCase() == '/organiser/organisationpastevents' ? classes.activeBar : ""} onClick={handleOrgPastEvents}>
                                            <ListItemIcon>

                                                <EventBusy className={location.pathname.toLowerCase() == '/organiser/organisationpastevents' ? classes.activeBar : ""} />
                                            </ListItemIcon>
                                            <ListItemText primary="Organisation Past Events" />
                                        </ListItem>
                                    </List>
                                </div> 

                                 </Popover>
                                 </ThemeProvider>






                            <ListItem button className={location.pathname.toLowerCase() == '/organiser/eventrequests' ? classes.activeBar : ""} onClick={handleEventRequests}>
                                <ListItemIcon>
                                    {NoOfEventRequests == 0 ?
                                        <EventNote className={location.pathname.toLowerCase() == '/organiser/eventrequests' ? classes.activeBar : ""} /> :
                                        <Badge badgeContent={NoOfEventRequests} color="secondary">
                                            <EventNote className={location.pathname.toLowerCase() == '/organiser/eventrequests' ? classes.activeBar : ""} />
                                        </Badge>}
                                </ListItemIcon>

                                <ListItemText primary="Event Requests" />
                            </ListItem>

                        
                        </>
                    ) : (
                        <></>
                    )}
                    {auth.accessToken ? (
                        <ListItem button onClick={handleLogout}>
                            <ListItemIcon>
                                <ExitToApp />
                            </ListItemIcon>
                            <ListItemText primary="Logout" />
                        </ListItem>
                    ) : (
                        <></>
                    )}
                </List>
            </Drawer>
            <main className={classes.content}>
                <div className={classes.drawerHeader} />
                {/* Content */}
            </main>
        </div>
    );
}

export default Sidebar;
