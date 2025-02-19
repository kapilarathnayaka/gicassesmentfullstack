import React, { useState } from "react";
import PropTypes from "prop-types";
import TextField from "@mui/material/TextField";

const TextBox = ({ label, type = "text", minLength, maxLength, required , value , onChange }) => {

  //const [value, setValue] = useState(valuex);
  const [error, setError] = useState(false);
  const [helperText, setHelperText] = useState("");

  const validateInput = (input) => {
    if (required && !input) {
      return { error: true, text: `${label} is required.` };
    }

    if (minLength && input.length < minLength) {
      return { error: true, text: `${label} must be at least ${minLength} characters.` };
    }

    if (maxLength && input.length > maxLength) {
      return { error: true, text: `${label} must be less than ${maxLength} characters.` };
    }

    if (type === "email" && !/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(input)) {
      return { error: true, text: "Invalid email format." };
    }

    if (type === "phone" && !/^[89]\d{7}$/.test(input)) {
      return { error: true, text: "Phone must start with 8 or 9 and be 8 digits long." };
    }

    return { error: false, text: "" };
  };

  const handleChange = (e) => {
    const inputValue = e.target.value;
    // setValue(inputValue);
    const validationResult = validateInput(inputValue);
    setError(validationResult.error);
    setHelperText(validationResult.text);
    onChange(inputValue);
  };

  return (
    <TextField
      label={label}
      variant="outlined"
      fullWidth
      value={value||""}
      onChange={handleChange}
      error={error}
      helperText={helperText}
      type={type === "email" ? "email" : "text"}
      inputProps={{ minLength, maxLength }}
      required={required}
      sx={{ marginBottom: 2 }}
    />
  );
};

export default TextBox;





TextBox.propTypes = {
  abel: PropTypes.string.isRequired,
  type: PropTypes.oneOf(["text", "email", "phone"]),
  minLength: PropTypes.number,
  maxLength: PropTypes.number,
  required: PropTypes.bool,
};

