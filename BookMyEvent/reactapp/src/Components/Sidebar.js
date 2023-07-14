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
import { useNavigate } from "react-router-dom";
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




    const auth = useSelector((store) => store.auth);
    const NoOfRequestedPeers = useSelector(store => store.organisers.NoOfRequestedOrganisers);
    let NoOfEventRequests = useSelector((state) => state.events.noOfEventRequests);

    const Profile = useSelector((state) => (state.profile.info));
    const logout = useLogout();
    const classes = useStyles();
    const [activeItem, setActiveItem] = useState('');


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
        setActiveItem("profile");
    };
    const handleAdd = async () => {
        handleDrawerClose();
        if (auth?.role == "Admin") navigate("/admin/addadmin");
        else navigate("/organiser/addSecondaryOwner");
        setActiveItem("addanother");
    };
    const handleRequests = async () => {
        handleDrawerClose();
        if (auth?.role == "Admin") navigate("/admin/Requests");

    };
    const handleOrganisations = async () => {
        handleDrawerClose();
        if (auth?.role == "Admin") navigate("/admin/Organisations");
        setActiveItem("organisations")
    };
    const handlePeerRequests = async () => {
        handleDrawerClose();
        if (auth.role == "Owner" || auth.role == "Secondary_Owner")
            navigate("organiser/PeerRequests");

        setActiveItem("peerrequests");
    };
    const handleOrganiseEvent = async () => {
        handleDrawerClose();
        navigate("/organiser/AddEvent");
        setActiveItem("createevent");
    };
    const handleOrganisationTree = async () => {
        const profile = store.getState().profile.info;
        handleDrawerClose();
        navigate(`/organiser/OrganisationTree/${profile.organisationId}`);
        setActiveItem("orgTree");
    };
    const handleLogin = async () => {
        handleDrawerClose();
        navigate("login");
        setActiveItem("login");
    };
    const handleGlobalHome = () => {
        handleDrawerClose();
        navigate("/");
        setActiveItem("globalhome");
    };
    const handleOrganiseAnEvent = async () => {
        handleDrawerClose();
        navigate("/organiser");
        setActiveItem("organiseanevent");

    };

    const handleMyPastEvents = async () => {
        handleDrawerClose();
        navigate("/organiser/myPastEvents");
        setActiveItem("mypastevents")
    };
    const handleOrgPastEvents = async () => {
        handleDrawerClose();
        navigate("/organiser/organisationPastEvents");
        setActiveItem("orgpastevents");
    };

    const handleEventRequests = async () => {
        handleDrawerClose();
        navigate("/organiser/eventReq");
        setActiveItem("eventreq");
    };

    const handleOrganisationEvents = async () => {
        handleDrawerClose();
        navigate("/organiser/organisationEvents");
        setActiveItem("orgevents");
    };
    const handleRegisteredEvents = async () => {
        handleDrawerClose();
        navigate("/registeredEvents");
        setActiveItem("registeredevents");
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
        setActiveItem("home");
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
                            <ListItem button className={activeItem == 'home' ? classes.activeBar : ""} onClick={handleHome}>
                                <ListItemIcon>

                                    <Home className={activeItem == 'home' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary="Home" />
                            </ListItem>
                            <ListItem button className={activeItem == 'profile' ? classes.activeBar : ""} onClick={handleProfile}>
                                <ListItemIcon >
                                    <AccountCircle className={activeItem == 'profile' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary="Profile" />
                            </ListItem>
                        </>
                    ) : (
                        <></>
                    )}
                    {auth.accessToken == "" ? (
                        <ListItem button className={activeItem == 'globalhome' ? classes.activeBar : ""} onClick={handleGlobalHome}>
                            <ListItemIcon>
                                <Home className={activeItem == 'globalhome' ? classes.activeBar : ""} />
                            </ListItemIcon>
                            <ListItemText primary="Home" />
                        </ListItem>
                    ) : (
                        <></>
                    )}
                    {auth.accessToken == "" ? (
                        <ListItem button className={activeItem == 'login' ? classes.activeBar : ""} onClick={handleLogin}>
                            <ListItemIcon>
                                <AccountCircle className={activeItem == 'login' ? classes.activeBar : ""} />
                            </ListItemIcon>
                            <ListItemText primary="Login" />
                        </ListItem>
                    ) : (
                        <></>
                    )}
                    {auth.accessToken == "" ? (
                        <ListItem button className={activeItem == 'organiseanevent' ? classes.activeBar : ""} onClick={handleOrganiseAnEvent}>
                            <ListItemIcon>
                                <PostAdd className={activeItem == 'organiseanevent' ? classes.activeBar : ""} />
                            </ListItemIcon>
                            <ListItemText primary="Organise an Event?" />
                        </ListItem>
                    ) : (
                        <></>
                    )}

                    {auth.role == "User" ? (
                        <ListItem button className={activeItem == 'registeredevents' ? classes.activeBar : ""} onClick={handleRegisteredEvents}>
                            <ListItemIcon>
                                <EventNote className={activeItem == 'registeredevents' ? classes.activeBar : ""} />
                            </ListItemIcon>
                            <ListItemText primary="Registered Events" />
                        </ListItem>
                    ) : (
                        <></>
                    )}
                    {auth.role == "Owner" && Profile.isAccepted == true || auth.role == "Secondary_Owner" ? (
                        <ListItem button className={activeItem == 'peerrequests' ? classes.activeBar : ""} onClick={handlePeerRequests}>
                            <ListItemIcon>
                                {NoOfRequestedPeers == 0 ?
                                    <GroupAdd className={activeItem == 'peerrequests' ? classes.activeBar : ""} />
                                    : <Badge badgeContent={NoOfRequestedPeers} color="secondary">
                                        <GroupAdd className={activeItem == 'peerrequests' ? classes.activeBar : ""} />
                                    </Badge>}
                            </ListItemIcon>
                            <ListItemText primary="Peer Requests" />
                        </ListItem>
                    ) : (
                        <></>
                    )}
                    {auth.role === "Admin" ? (
                        <ListItem button className={activeItem == 'addanother' ? classes.activeBar : ""} onClick={handleAdd}>
                            <ListItemIcon>
                                <GroupAdd className={activeItem == 'addanother' ? classes.activeBar : ""} />
                            </ListItemIcon>
                            <ListItemText primary="Add Admin" />
                        </ListItem>
                    ) : (
                        <></>
                    )}
                    {auth.role === "Admin" ? (
                        <>
                            <ListItem button className={activeItem == 'organisations' ? classes.activeBar : ""} onClick={handleOrganisations}>
                                <ListItemIcon>
                                    <AccountTree className={activeItem == 'organisations' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary="Organisations" />
                            </ListItem>
                            <ListItem button className={activeItem == 'categories' ? classes.activeBar : ""} onClick={handleCategories}>
                                <ListItemIcon>
                                    <Category className={activeItem == 'categories' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary="Categories" />
                            </ListItem>
                            <ListItem button className={activeItem == 'usersList' ? classes.activeBar : ""} onClick={handleUsersList}>
                                <ListItemIcon>
                                    <People className={activeItem == 'usersList' ? classes.activeBar : ""} />
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
                        <ListItem button className={activeItem == 'createevent' ? classes.activeBar : ""} onClick={handleOrganiseEvent}>
                            <ListItemIcon>
                                <PostAdd className={activeItem == 'createevent' ? classes.activeBar : ""} />
                            </ListItemIcon>
                            <ListItemText primary="Organise Event" />
                        </ListItem>
                    ) : (
                        <></>
                    )}
                    {/* {auth.role === "Owner" && Profile.isAccepted == true ? (
                        <ListItem button className={activeItem == 'addanother' ? classes.activeBar : ""} onClick={handleAdd}>
                            <ListItemIcon>
                                <PersonAdd className={activeItem == 'addanother' ? classes.activeBar : ""} />
                            </ListItemIcon>
                            <ListItemText primary="Add Secondary Owner" />
                        </ListItem>
                    ) : (
                        <></>
                    )} */}
                    {auth.role == "Owner" && Profile.isAccepted == true ||
                        (auth.role == "Peer" && Profile.isAccepted == true) ||
                        auth.role == "Secondary_Owner" ? (
                        <>
                            <ListItem button className={activeItem == 'orgTree' ? classes.activeBar : ""} onClick={handleOrganisationTree}>
                                <ListItemIcon>

                                    <AccountTree className={activeItem == 'orgTree' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary="Organisation Tree" />
                            </ListItem>

                            <ListItem
                                button
                                className={activeItem == 'orgevents' || activeItem == 'mypastevents' || activeItem == 'orgpastevents' ? classes.activeBar : ""}

                                onClick={handleItemClick}

                            >
                                <ListItemIcon className={activeItem == 'orgevents' || activeItem == 'mypastevents' || activeItem == 'orgpastevents' ? classes.activeBar : ""} >
                                    <AccountTreeIcon className="" />
                                </ListItemIcon>
                                <ListItemText primary="Events" />
                                {/* <ListItemSecondaryAction   className={activeItem == 'orgevents' || activeItem == 'mypastevents' || activeItem == 'orgpastevents' ? classes.activeBar : ""}> */}
                                {anchorEl == null ? <><ArrowForwardIosTwoTone onClick={handleItemClick} style={{ fontSize: '1rem' }} /></> : <><ArrowBackIos style={{ fontSize: '1rem' }} /></>}

                                {/* </ListItemSecondaryAction> */}
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
                                        <ListItem button className={activeItem == 'orgevents' ? classes.activeBar : ""} onClick={handleOrganisationEvents}>
                                            <ListItemIcon>
                                                <EventAvailable className={activeItem == 'orgevents' ? classes.activeBar : ""} />
                                            </ListItemIcon>
                                            <ListItemText primary="Active Organisation Events" />
                                        </ListItem>
                                        <ListItem button className={activeItem == 'mypastevents' ? classes.activeBar : ""} onClick={handleMyPastEvents}>
                                            <ListItemIcon>
                                                <Event className={activeItem == 'mypastevents' ? classes.activeBar : ""} />
                                            </ListItemIcon>
                                            <ListItemText primary="My Past events" />
                                        </ListItem>
                                        <ListItem button className={activeItem == 'orgpastevents' ? classes.activeBar : ""} onClick={handleOrgPastEvents}>
                                            <ListItemIcon>

                                                <EventBusy className={activeItem == 'orgpastevents' ? classes.activeBar : ""} />
                                            </ListItemIcon>
                                            <ListItemText primary="Organisation Past Events" />
                                        </ListItem>
                                    </List>
                                </div> 

                                 </Popover>
                                 </ThemeProvider>





                            {/* <ListItem button className={activeItem == 'mypastevents' ? classes.activeBar : ""} onClick={handleMyPastEvents}>
                                <ListItemIcon>
                                    <Event className={activeItem == 'mypastevents' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary="My Past Events" />
                            </ListItem> */}

                            {/* <ListItem button className={activeItem == 'orgpastevents' ? classes.activeBar : ""} onClick={handleOrgPastEvents}>
                                <ListItemIcon>

                                    <EventBusy className={activeItem == 'orgpastevents' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary="Organisation Past Events" />
                            </ListItem> */}

                            <ListItem button className={activeItem == 'eventreq' ? classes.activeBar : ""} onClick={handleEventRequests}>
                                <ListItemIcon>
                                    {NoOfEventRequests == 0 ?
                                        <EventNote className={activeItem == 'eventreq' ? classes.activeBar : ""} /> :
                                        <Badge badgeContent={NoOfEventRequests} color="secondary">
                                            <EventNote className={activeItem == 'eventreq' ? classes.activeBar : ""} />
                                        </Badge>}
                                </ListItemIcon>

                                <ListItemText primary="Event Requests" />
                            </ListItem>

                            {/* <ListItem button className={activeItem == 'orgevents' ? classes.activeBar : ""} onClick={handleOrganisationEvents}>
                                <ListItemIcon>
                                    <EventAvailable className={activeItem == 'orgevents' ? classes.activeBar : ""} />
                                </ListItemIcon>
                                <ListItemText primary=" Active Organisation Events" />
                            </ListItem> */}
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
