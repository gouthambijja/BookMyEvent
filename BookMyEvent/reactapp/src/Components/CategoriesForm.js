import React, { useState } from 'react'

import { Button, FormControl, TextField } from "@material-ui/core";
import { useDispatch, useSelector } from 'react-redux';
import CategoryServices from '../Services/CategoryServices';
import { addCategoryThunk } from '../Features/ReducerSlices/CategorySlice';
import { toast } from 'react-toastify';

const CategoriesForm = () => {
    const categories = useSelector(store => store.category.categories);
    const [categoryErrorMsg,setCategoryErrorMsg] = useState("");
    const [category,setCategory] = useState(""); 
    const dispatch = useDispatch();
    const handleAddCategory = async (e) =>{
      e.preventDefault();
      if(categoryErrorMsg === "" && category!==""){
        try{
          await dispatch(addCategoryThunk({categoryName:category})).unwrap();
          toast.success("Category Added Succesfully");
        }
        catch{
            toast.warning('category adding failed')
        }
        setCategory("");
      }
    }
    const handleCategoryNameInput = (e) =>{
        const newCategory = e.target.value;
        setCategory(newCategory);
        if(categories.find(a => a.categoryName == newCategory)){
            setCategoryErrorMsg("Category already present!");
        }
        else {
            setCategoryErrorMsg("");
        }
        
    }
  return (
    <FormControl
          style={{
            border: "1px solid #d0d0d0",
            width: "100%",
            borderRadius: "4px",
            padding: "20px",            
          }}
        >
          <TextField
        label="Category"
        name="Category"
        value={category}
        error={categoryErrorMsg !== ""}
        helperText={categoryErrorMsg}
        onChange={handleCategoryNameInput}
        variant="outlined"
        fullWidth
        required
      />
          <Button variant="outlined" fullWidth onClick={handleAddCategory} style={{marginTop:'10px',background:'#3f50b5',color:'white'}}>Add Category </Button>
        </FormControl>
  )
}

export default CategoriesForm
