import React, { useEffect, useState } from 'react'
import { useSelector } from 'react-redux';
import OwnerRequests from './OwnerRequests';

const AdminHomePage = () => {
const [fakeString,setFakeString] = useState(['hey']);
const admin = useSelector(store => store.profile.info)
//console.log(admin);
 
  return (
    <OwnerRequests/>
  )
}

export default AdminHomePage
