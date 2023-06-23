import React from 'react'
import Navbar from './Navbar'
import { Outlet, useLoaderData } from 'react-router-dom'
import Sidebar from './Sidebar'
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const Layout = () => {
  return (
    <div >
      <Navbar/>
      <div style={{paddingTop:'65px'}}>
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
      <Outlet/></div>
    </div>
  )
}

export default Layout
