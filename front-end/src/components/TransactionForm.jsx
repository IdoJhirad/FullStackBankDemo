import React, {useState} from "react";
import './TransactionForm.css';
export const TransactionForm = ({fields, onSubmit, title, submitText}) => {
    const [formData, setFormData] = useState(
        fields.reduce((acc, field)=>{
            acc[field.name] = "";
            return acc;
        })
    );
    const handleChange =(e) => {
        const {name, value} = e.target;
        setFormData({...formData, [name]:value });
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        onSubmit(formData);
    }
    return (
        <>
        <form className="transaction-form" onSubmit={handleSubmit}>
           <h2>{title}</h2>
           {fields.map((field) => (
          <div key={field.name}  className="form-row">
            <label htmlFor={field.name}>
              {field.label}
            </label>
            {field.type === "select" ? (
              <select
                id={field.name}
                name={field.name}
                value={formData[field.name]}
                onChange={handleChange}
                required={field.required}
              
              >
                {field.options.map((option) => (
                  <option key={option.value} value={option.value}>
                    {option.label}
                  </option>
                ))}
              </select>
            ) : (
              <input
                id={field.name}
                name={field.name}
                placeholder={field.placeholder}
                value={formData[field.name]}
                onChange={handleChange}
                required={field.required}
                type={field.type}
               
              />
            )}
          </div>
            ))}
        <button
            type="submit"
           
            >
            {submitText}
        </button>
        </form>
        </>
    )
}