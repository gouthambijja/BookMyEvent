import React from 'react'
import { useSelector } from 'react-redux'
import OrganiserLandingPage from '../Components/OrganiserLandingPage'
import VerificationPage from '../pages/VerificationPage'

const OrganiserHomePage = () => {
    const Peer = useSelector((state) => (state.profile.info));
    console.log(Peer.isAccepted);
    return (
        Peer?.isAccepted == true ? <OrganiserLandingPage /> : <VerificationPage />
    )
}
export default OrganiserHomePage
