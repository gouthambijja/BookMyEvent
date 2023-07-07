import React from 'react'
import AddEventForm from '../Components/AddEventForm'

const AddEvent = () => {
const styles = {
    container:{
        width:'100%',
        padding:'30px'
    }
}
  return (
    <div style={styles.container}>
        <AddEventForm/>
    </div>
  )
}

export default AddEvent
