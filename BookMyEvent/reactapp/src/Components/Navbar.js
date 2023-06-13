import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import IconButton from "@mui/material/IconButton";
import Sidebar from "./Sidebar";
import { Menu } from "@material-ui/icons";
import { useState } from "react";
import RequireAuth from "./RequireAuth";

export default function Navbar() {
  const [open, setOpen] = useState(false);


  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static" style={{ background: "#3f51b5" }}>
        <Toolbar>
          <IconButton
            size="large"
            edge="start"
            color="inherit"
            aria-label="menu"
            sx={{ mr: 2 }}
            onClick={() => {
              setOpen(!open);
            }}
          >
            <RequireAuth>
            <Menu />
            </RequireAuth>
          </IconButton>

          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            BookMyEvent
          </Typography>
        </Toolbar>
      </AppBar>
      <div style={{position:'absolute',zIndex:'100' }}>
        <Sidebar open={open} setOpen={setOpen} />
      </div>
    </Box>
  );
}
