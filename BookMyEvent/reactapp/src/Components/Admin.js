import React from 'react'
import Navbar from './Navbar'
import { Outlet } from 'react-router-dom'
import Login from './Login'

const Admin = () => {

  return (
    <div className='ExternalContainer'>
        <Outlet/>
    </div>
  )
}

export default Admin
