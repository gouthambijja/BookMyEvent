import React from 'react'
import { Outlet } from 'react-router-dom'

const Organiser = () => {

  return (
    <div className='ExternalContainer'>
        <Outlet/>
    </div>
  )
}

export default Organiser
