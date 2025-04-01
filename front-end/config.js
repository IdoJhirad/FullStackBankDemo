export const TransactionFields = [
      {
        name: "hebrewName",
        label: "Hebrew Name",
        placeholder: "Enter Hebrew Name",
        type: "text",
        required: true,
      },
      {
        name: "englishName",
        label: "English Name",
        placeholder: "Enter English Name",
        type: "text",
        required: true,
      },
      {
        name: "idNumber",
        label: "ID Number",
        placeholder: "Enter 9-digit ID Number",
        type: "text",
        required: true,
      },
      {
        name: "accountNumber",
        label: "Account Number",
        placeholder: "Enter Account Number",
        type: "text",
        required: true,
      },
      {
        name: "birthDate",
        label: "Birth Date",
        placeholder: "",
        type: "date",
        required: true,
      },
      {
        name: "amount",
        label: "Amount",
        placeholder: "Enter Amount",
        type: "number",
        required: true,
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
  ];