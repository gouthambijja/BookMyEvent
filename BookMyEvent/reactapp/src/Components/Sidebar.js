import React, { useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import store from "../App/store";

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
  SpeakerGroup,
  AccountTree,
  People,
  EmojiPeople,
  NaturePeople,
  PeopleAlt,
  VerifiedUser,
  SupervisedUserCircle,
  SupervisorAccount,
} from "@material-ui/icons";

import { useNavigate } from "react-router-dom";
import useLogout from "../Hooks/Logout";
import { useSelector } from "react-redux";

const drawerWidth = 240;

const useStyles = makeStyles((theme) => ({
  root: {
    display: "flex",
  },
  appBar: {
    transition: theme.transitions.create(["margin", "width"], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
  },
  activeBar:{
    '&:hover': {
      backgroundColor:theme.palette.primary.main,
    color:"white",
    }, 
    backgroundColor:theme.palette.primary.main,
    color:"white",
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
  const logout = useLogout();
  const classes = useStyles();
  const [activeItem, setActiveItem] = useState('');
  //   const [open, setOpen] = useState(false);

  const handleDrawerOpen = () => {
    setOpen(true);
  };
  const handleItemClick = (item) => {
    setActiveItem(item);
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
  const handleGlobalHome= () => {
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
  const handleCategories = async() =>{
    handleDrawerClose();
    navigate("/Admin/categories")
  }
  return (

    <div className={classes.root}>
      {open?<div style={{width:'100vw',height:'100vh',position:'absolute',background:'rgba(0,0,0,0.1)',zIndex:'40'}} onClick={handleDrawerClose}></div>:<></>}
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
        <List>
          {auth.accessToken ? (
            <>
              <ListItem button className={activeItem == 'home'?classes.activeBar:""} onClick={handleHome}>
                <ListItemIcon>
                
                <Home className={activeItem == 'home'?classes.activeBar:""} />
                </ListItemIcon>
                <ListItemText primary="Home" />
              </ListItem>
              <ListItem button className={activeItem == 'profile'?classes.activeBar:""} onClick={handleProfile}>
                <ListItemIcon >
                  <AccountCircle className={activeItem == 'profile'?classes.activeBar:""} />
                </ListItemIcon>
                <ListItemText primary="Profile" />
              </ListItem>
            </>
          ) : (
            <></>
          )}
          {auth.accessToken == "" ? (
            <ListItem button className={activeItem == 'globalhome'?classes.activeBar:""} onClick={handleGlobalHome}>
              <ListItemIcon>
                <Home className={activeItem == 'globalhome'?classes.activeBar:""}  />
              </ListItemIcon>   
              <ListItemText primary="Home" />
            </ListItem>
          ) : (
            <></>
            )}
            {auth.accessToken == "" ? (
              <ListItem button className={activeItem == 'login'?classes.activeBar:""} onClick={handleLogin}>
                <ListItemIcon>
                  <AccountCircle className={activeItem == 'login'?classes.activeBar:""} />
                </ListItemIcon>
                <ListItemText primary="Login" />
              </ListItem>
            ) : (
              <></>
            )}
          {auth.accessToken == ""  ? (
            <ListItem button className={activeItem == 'organiseanevent'?classes.activeBar:""} onClick={handleOrganiseAnEvent}>
              <ListItemIcon>
              <PostAdd className={activeItem == 'organiseanevent'?classes.activeBar:""} />
              </ListItemIcon>
              <ListItemText primary="Organise an Event?" />
            </ListItem>
          ) : (
            <></>
          )}

          {auth.role == "User" ? (
            <ListItem button className={activeItem == 'registeredevents'?classes.activeBar:""} onClick={handleRegisteredEvents}>
              <ListItemIcon>
              <EventNote className={activeItem == 'registeredevents'?classes.activeBar:""} />
              </ListItemIcon>
              <ListItemText primary="Registered Events" />
            </ListItem>
          ) : (
            <></>
          )}
          {auth.role == "Owner" || auth.role == "Secondary_Owner" ? (
            <ListItem button className={activeItem == 'peerrequests'?classes.activeBar:""} onClick={handlePeerRequests}>
              <ListItemIcon>
                <GroupAdd className={activeItem == 'peerrequests'?classes.activeBar:""} />
              </ListItemIcon>
              <ListItemText primary="Peer Requests" />
            </ListItem>
          ) : (
            <></>
          )}
          {auth.role === "Admin" ? (
            <ListItem button className={activeItem == 'addanother'?classes.activeBar:""} onClick={handleAdd}>
              <ListItemIcon>
                <GroupAdd className={activeItem == 'addanother'?classes.activeBar:""} />
              </ListItemIcon>
              <ListItemText primary="Add Admin" />
            </ListItem>
          ) : (
            <></>
          )}
          {auth.role === "Admin" ? (
            <>
            <ListItem button className={activeItem == 'organisations'?classes.activeBar:""} onClick={handleOrganisations}>
              <ListItemIcon>
              <AccountTree className={activeItem == 'organisations'?classes.activeBar:""} />
              </ListItemIcon>
              <ListItemText primary="Organisations" />
            </ListItem>
            <ListItem button className={activeItem == 'categories'?classes.activeBar:""} onClick={handleCategories}>
              <ListItemIcon>
              <Category className={activeItem == 'categories'?classes.activeBar:""} />
              </ListItemIcon>
              <ListItemText primary="Categories" />
            </ListItem></>
          ) : (
            <></>
          )}
          {auth.role == "Owner" ||
          auth.role == "Peer" ||
          auth.role == "Secondary_Owner" ? (
            <ListItem button className={activeItem == 'createevent'?classes.activeBar:""} onClick={handleOrganiseEvent}>
              <ListItemIcon>
                <PostAdd className={activeItem == 'createevent'?classes.activeBar:""} />
              </ListItemIcon>
              <ListItemText primary="Organise Event" />
            </ListItem>
          ) : (
            <></>
          )}
          {auth.role === "Owner" ? (
            <ListItem button className={activeItem == 'addanother'?classes.activeBar:""} onClick={handleAdd}>
              <ListItemIcon>
                <PersonAdd className={activeItem == 'addanother'?classes.activeBar:""} />
              </ListItemIcon>
              <ListItemText primary="Add Secondary Owner" />
            </ListItem>
          ) : (
            <></>
          )}
          {auth.role == "Owner" ||
          auth.role == "Peer" ||
          auth.role == "Secondary_Owner" ? (
            <>
              <ListItem button className={activeItem == 'orgTree'?classes.activeBar:""} onClick={handleOrganisationTree}>
                <ListItemIcon>
                  
                <AccountTree className={activeItem == 'orgTree'?classes.activeBar:""} />
                </ListItemIcon>
                <ListItemText primary="Organisation Tree" />
              </ListItem>

              <ListItem button className={activeItem == 'mypastevents'?classes.activeBar:""} onClick={handleMyPastEvents}>
                <ListItemIcon>
                  <Event className={activeItem == 'mypastevents'?classes.activeBar:""}/>
                </ListItemIcon>
                <ListItemText primary="My Past Events" />
              </ListItem>

              <ListItem button className={activeItem == 'orgpastevents'?classes.activeBar:""} onClick={handleOrgPastEvents}>
                <ListItemIcon>
                  
                  <EventBusy className={activeItem == 'orgpastevents'?classes.activeBar:""} />
                </ListItemIcon>
                <ListItemText primary="Organisation Past Events" />
              </ListItem>

              <ListItem button className={activeItem == 'eventreq'?classes.activeBar:""} onClick={handleEventRequests}>
                <ListItemIcon>
                  <EventNote className={activeItem == 'eventreq'?classes.activeBar:""} />
                </ListItemIcon>
                <ListItemText primary="Event Requests" />
              </ListItem>

              <ListItem button className={activeItem == 'orgevents'?classes.activeBar:""} onClick={handleOrganisationEvents}>
                <ListItemIcon>
                <EventAvailable className={activeItem == 'orgevents'?classes.activeBar:""} />
                </ListItemIcon>
                <ListItemText primary=" Active Organisation Events" />
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
