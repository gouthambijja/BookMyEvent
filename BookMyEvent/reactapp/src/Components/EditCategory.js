import { Check, Close, Edit, Stop } from "@mui/icons-material";
import { TextField } from "@mui/material";
import { isEditable } from "@testing-library/user-event/dist/utils";
import React, { useState } from "react";

const EditCategory = ({ category }) => {
  const handleEdit = category.handleEdit;
  const handleCancel = category.handleCancel;
  const handleSave = category.handleSave;
  const index = category.index;
  const _category = category.categories[index];
  const isEditable = category.IsEditable;
  const [categoryState, setCategoryState] = useState("");
  const [categoryErrorMsg, setCategoryErrorMsg] = useState("");
  const handleCategoryNameInput = (e) => {
    setCategoryState(e.target.value);
    console.log(e.target.value, _category.categoryName);
    if (category.categories.find((a) => a.categoryName == e.target.value)) {
      setCategoryErrorMsg(
        "Category Name shouldn't present in the previous categories!"
      );
    } else {
      setCategoryErrorMsg("");
    }
  };
  const Button = {
    "&:hover": {
      cursor: "pointer",
      color: "rgb(80,80,80)",
    },
  };
  return (
    <>
      <div
        style={{
          whiteSpace: "nowrap",
          overflow: "hidden",
          textOverflow: "ellipsis",
          flexBasis: "80%",
        }}
      >
        {!isEditable ? (
          _category.categoryName
        ) : (
          <TextField
            label="Edit Category"
            name="Edit Category"
            value={categoryState}
            error={categoryErrorMsg !== ""}
            helperText={categoryErrorMsg}
            onChange={handleCategoryNameInput}
            variant="outlined"
            fullWidth
            required
          />
        )}
      </div>

      <span
        style={{
          flexBasis: "20%",
          display: "flex",
          justifyContent: "end",
          right: !isEditable ? "10px" : "0px",
          color: "#a2a2a2",
        }}
      >
        {!isEditable ? (
          <Edit
            sx={Button}
            onClick={() => {
              handleEdit(index);
            }}
          />
        ) : (
          <>
            <span style={{ display: "flex" }}>
              <span style={{ padding: "10px" }}>
                <Check
                  sx={Button}
                  onClick={() => {
                    if (categoryErrorMsg === "") {
                      handleSave(index, categoryState);
                    }
                  }}
                />
              </span>
              <span style={{ padding: "10px" }}>
                <Close
                  sx={Button}
                  onClick={() => {
                    handleCancel(index);
                  }}
                />
              </span>
            </span>
          </>
        )}
      </span>
    </>
  );
};

export default EditCategory;
