import React from 'react'
import Navbar from './Navbar'
import { Outlet } from 'react-router-dom'
import Login from './AdminLogin'

const Admin = () => {

  return (
    <div>
        <Outlet/>
    </div>
  )
}

export default Admin
