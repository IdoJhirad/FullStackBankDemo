export const TransactionFields = [
      {
        name: "hebrewName",
        label: "Hebrew Name",
        placeholder: "Enter Hebrew Name",
        type: "text",
        required: true,
        validate: (value) => { 
          if (value.length > 20) {
            return "Hebrew Name should contain no more than 20 characters.";
          }

          const hebrewRegex = /^(?=.*[\u05D0-\u05EA])[\u05D0-\u05EA\s'-]{1,20}$/;
          if (!hebrewRegex.test(value)) {
            return "Hebrew Name must contain at least one Hebrew letter and only allowed characters.";
          }
          return null;
        }
      },
      {
        name: "englishName",
        label: "English Name",
        placeholder: "Enter English Name",
        type: "text",
        required: true,
        validate: (value) => {
        
          if (value.length > 15) {
            return "English Name should contain no more than 15 characters.";
          }

          const englishRegex = /^(?=.*[A-Za-z])[A-Za-z\s'-]{1,15}$/;
          if (!englishRegex.test(value)) {
            return "English Name should contain only English letters.";
          }
          return null;
        }
      },
      {
        name: "idNumber",
        label: "ID Number",
        placeholder: "Enter 9-digit ID Number",
        type: "text",
        required: true,
        validate: (value) => {
            const idRegex = /^\d{9}$/;
            if (!idRegex.test(value)) {
              return "ID Number must be exactly 9 digits.";
            }
            return null;
        }
      },
      {
        name: "accountNumber",
        label: "Account Number",
        placeholder: "Enter Account Number",
        type: "text",
        required: true,
        validate: (value) => {
          const accountRegex = /^\d{1,9}$/;
          if (!accountRegex.test(value)) {
            return "Account Number must be 1 to 9 digits.";
          }
          return null;
        }
      },
      {
        name: "birthDate",
        label: "Birth Date",
        placeholder: "",
        type: "date",
        required: true,
        validate: (value) => {
          const date = new Date(value);
          const today = new Date();
          let age = today.getFullYear() - date.getFullYear();
          const monthDiff = today.getMonth() - date.getMonth();
          if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < date.getDate())) {
            age--;
          }
          if (age < 18) {
            return "Must be at least 18 years old.";
          }
          if (date > today) {
            return "Birth date cannot be in the future";
       
          }
          return null;
        }
      },
      {
        name: "amount",
        label: "Amount",
        placeholder: "Enter Amount",
        type: "number",
        required: true,
        validate: (value) => {
          if(isNaN(value) || Number(value) <= 0 ) {
            return "Amount shold be a number greater then 0";
          }
          return null;
        }
      },
      {
        name: "type",
        label: "Transaction Type",
        placeholder: "Select Transaction Type",
        type: "select",
        required: true,
        options: [
          { value: "", label: "-- Select --" },
          { value: "Deposit", label: "Deposit" },
          { value: "Withdrawal", label: "Withdrawal" },
        ],
      },
  ];


  export const TableHeaders = [
    "ID",
    "Hebrew Name",
    "English Name",
    "ID Number",
    "Account Number",
    "Birth Date",
    "Amount",
    "Date",
    "Type",
    "Status",
    "Is Edited",
    "Is Deleted",
    "Actions"
  ];