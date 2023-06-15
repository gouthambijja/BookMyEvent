import React from 'react'
import { useSelector } from 'react-redux'
import { Outlet,useNavigate,Navigate, useLocation } from 'react-router-dom'


const RequireAuth = ({allowedrole}) => {
    const auth = useSelector(store => store.auth);
    const location = useLocation();
  return (
    <>  {
        
      allowedrole.includes(auth.role)?
        <Outlet/>:<Navigate to="login" replace={true} state= {location}> </Navigate>
        }

    </>
  )
}

export default RequireAuth
