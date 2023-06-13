import React from 'react'
import { useSelector } from 'react-redux'
import { Outlet,useNavigate,Navigate } from 'react-router-dom'


const RequireAuth = ({allowedrole}) => {
    const auth = useSelector(store => store.auth);
  return (
    <>  {
        
      allowedrole == auth.role?
        <Outlet/>:<Navigate to="/admin" replace={true}></Navigate>
        }

    </>
  )
}

export default RequireAuth
