import React, { useEffect, useState } from 'react'
import { useSelector } from 'react-redux';

const AdminHomePage = () => {
const [fakeString,setFakeString] = useState(['hey']);
const admin = useSelector(store => store.profile.info)
console.log(admin);
 
  return (
    <div>
      AdminHomePage<br></br>
     
    </div>
  )
}

export default AdminHomePage
