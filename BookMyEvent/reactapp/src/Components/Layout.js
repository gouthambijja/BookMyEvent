import React from 'react'
import Navbar from './Navbar'
import { Outlet, useLoaderData } from 'react-router-dom'
import Sidebar from './Sidebar'
import { toast, ToastContainer } from 'react-toastify';
import { Container, Typography } from '@mui/material';

import 'react-toastify/dist/ReactToastify.css';
const footerStyle = {
  backgroundColor: '#3f51b5',
  color: '#fff',
  padding: '16px',
};
const Layout = () => {
  return (
    <div >
      <Navbar/>
      <div style={{paddingTop:'64px',minHeight:'calc( 100vh - 52px) '}}>
      <ToastContainer
        position="bottom-right"
        autoClose={5000}
        hideProgressBar={true}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss={false}
        draggable
        pauseOnHover
        closeButton={false}
      />
      <Outlet/>
      
      </div>
      <footer style={footerStyle}>
      <Container maxWidth="md">
        <Typography variant="body2" align="center">
          © {new Date().getFullYear()} BookMyEvent. All rights reserved.
        </Typography>
      </Container>
    </footer>
    </div>
  )
}

export default Layout
