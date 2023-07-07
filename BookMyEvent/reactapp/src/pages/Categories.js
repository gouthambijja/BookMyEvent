import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Edit } from "@mui/icons-material";
import CategoriesForm from "../Components/CategoriesForm";
import {
  editCategoryThunk,
  getCategoryThunk,
} from "../Features/ReducerSlices/CategorySlice";
import EditCategory from "../Components/EditCategory";
import { toast } from "react-toastify";

const Categories = () => {
  const categories = useSelector((store) => store.category.categories);
  var isEditableInitial = [];
  for (var i = 0; i < categories?.length; i++) {
    isEditableInitial.push(false);
  }
  const [isEditable, setIsEditable] = useState(isEditableInitial);
  const dispatch = useDispatch();
  const handleEdit = (index) => {
    let inputToggleCopy = [...isEditable];
    inputToggleCopy[index] = true;
    setIsEditable(inputToggleCopy);
  };
  const handleCancel = (index) => {
    let inputToggleCopy = [...isEditable];
    inputToggleCopy[index] = false;
    setIsEditable(inputToggleCopy);
  };
  const handleSave = async (index, category) => {
    try{
   await dispatch(
      editCategoryThunk({
        categoryId: categories[index].categoryId,
        categoryName: category,
      })
    ).unwrap();
    handleCancel(index);
    toast.success('category edit succesful');
    }
    catch{
      toast.warning("category edit failed");
    }
  };
  let cnt = 0;
  return (
    <div style={{ padding: "40px" }}>
      <div style={{ maxWidth: "800px", margin: "0px auto" }}>
        <CategoriesForm />
        <br></br>
        {categories.map((category, index) => (
          <div
            key={cnt++}
            style={{
              border: "1px solid #d0d0d0",
              width: "100%",
              display: "flex",
              marginTop: "4px",
              borderRadius: "4px",
              padding: "20px",
              zIndex: "2",
            }}
          >
            <EditCategory
              category={{
                categories: categories,
                IsEditable: isEditable[index],
                handleEdit: handleEdit,
                index: index,
                handleCancel: handleCancel,
                handleSave: handleSave,
              }}
            />
          </div>
        ))}
      </div>
    </div>
  );
};

export default Categories;
