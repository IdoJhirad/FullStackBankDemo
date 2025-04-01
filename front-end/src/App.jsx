import { useEffect, useState } from 'react'
import './App.css'
import apiClient from './services/axios.js'
import { GenericTable } from './components/GenericTable.jsx';
import { TransactionForm  } from './components/TransactionForm.jsx'
import {TransactionFields ,TableHeaders } from '../config.js'
import { Filter } from './components/Filter.jsx'
import { Pagination } from './components/Pagination.jsx'
import { Layout } from './Layout/Layout.jsx';
function App() {

  const [showPostForm, setShowPostForm] = useState(false);
  
  const [transactionsData, setTransactionsData] = useState([]);
  const [transactionLoading, setTransactionLoading] = useState(false);
  const [getTransactionError, setGetTransactionError] = useState(null);
  
  const [postMessage ,setPostMessage] = useState("");
  const [postErrorMessage, setPostErrorMessage] = useState("");

  const [filters, setFilters] = useState({
    isDeleted: null,
    isEdited: null,
    fromDate: '',
    toDate: '',
    sortBy: '',
    isDescending: null,
    type: '',
    pageNumber: 1, 
    pageSize: 20,
    
  });
 
  useEffect(() => {
    getTransaction();
  }, [filters]); 

  const getTransaction = async() => {
    setTransactionLoading(true);
    setGetTransactionError(null);

    try {
     
      const resp = await apiClient.get(`${import.meta.env.VITE_API_GET_TRANSACTION}`,{
        params: {
          isDeleted: filters.isDeleted,
          isEdited: filters.isEdited,
          fromDate: filters.fromDate,
          toDate: filters.toDate,
          sortBy: filters.sortBy,
          isDescending: filters.isDescending,
          type: filters.type,
          pageNumber: filters.pageNumber,
          pageSize: filters.pageSize
        }
    })
    if(resp.status == 200) {
      setTransactionsData(resp.data);
    }
    else {
      setGetTransactionError("somthing went wrong ");
    }
  }
  catch(err){
    if (err.response) {
      console.error("Status:", err.response.status);
      console.error("Data:", err.response.data);
    } else if (err.request) {
      // Request was made but no response received
      console.error("No response received:", err.request);
    } else {
      // Something else happened while setting up the request
      console.error("Error message:", err.message);
    }
    setGetTransactionError("somthing went worng");
  } 
  finally {
    setTransactionLoading(false); 
  }
}
const handleInputChange = (e) => {
  const { name, value, type, checked } = e.target;
  setFilters((prev) => ({
    ...prev,
    [name]: type === 'checkbox' ? checked : value,
  }));
};

const handleFilterSubmit = (e) => {
    e.preventDefault();
    setFilters((filter) => ({ ...filter, pageNumber: 1 }));
    //getTransaction();
  }

  const handleTransacationSubmit = async (data) => {
      setPostMessage(null);
      setPostErrorMessage(null);
      let endpoint = "";
      if(data.type == "Deposit") {
          endpoint = import.meta.env.VITE_API_DEPOSIT_ENDPOINT;
      } else if(data.type == "Withdrawal") {
        endpoint = import.meta.env.VITE_API_WITHDRAWAL_ENDPOINT
      } else {
        setPostErrorMessage("need to choos deposite ot withdrawal");
        return;
      }
      const now = new Date();
      const selectedDate = new Date(data.birthDate);
      if (selectedDate > now) {
        setPostErrorMessage("Birth date cannot be in the future");
        return;
      }

      try {

        const response = await apiClient.post(endpoint, data);
        if(response.status == 201)
        {
          setPostMessage(`${data.type} sucseed`);
        } else {
          setPostMessage(`${data.type} faild`);
        }
        setShowPostForm(false);
        getTransaction();
      }
        catch (err) {
          console.log("Full error response:", err.response);
        
          if (err.response) {

            const data = err.response.data;

            if (Array.isArray(data)) {
              console.log("you are in the first");
              const messages = data
                .map((item) => item.description || JSON.stringify(item))
                .join(" ");
                setPostErrorMessage(messages);
              //if the error is an object
            } else if (typeof data === "object" && data !== null) {
              console.log("you are in the in the second");
              
              if (data.errors) {
                console.log("you are in the second firs");
                // Flatten them into one string
                const flattened = Object.entries(data.errors)
                  .map(([field, msgs]) => `${msgs.join(", ")
                  }`)
                  .join("\n");
                  setPostErrorMessage(flattened);
                
  
            }
            else {
              if(err.response.status == 401){
                setPostErrorMessage("can't get a token from third party pleas try again later");
              } else{
                data.status ?  setPostErrorMessage(String(data.status)):  setPostErrorMessage("An unexpected error occurred.");;
              }
              console.log("you are in the third");
            }
          } else {
            console.error("Error setting up request:", err.message);
            setPostErrorMessage("An unexpected error occurred.");
          }
        }
      }
    }
    const handlePageChange = (newPage) => {
      setFilters((prev) => ({ ...prev, pageNumber: newPage }));
      //getTransaction();
    };

    const handlePageSizeChange = (newSize) => {
      setFilters((prev) => ({ ...prev, pageSize: newSize, pageNumber: 1 }));
    };

  return (
    <Layout>
    <div>
      <button onClick={()=> setShowPostForm(!showPostForm)}>Add Transaction</button>
      {showPostForm &&<TransactionForm fields={TransactionFields} onSubmit={handleTransacationSubmit} title={"send Transaction"} submitText={"send Transaction"}/> }
      {postMessage && <div>{postMessage}</div>}
      {postErrorMessage && (
      <div style={{ whiteSpace: 'pre-wrap', color: 'red' }}>
        {postErrorMessage}
      </div>)}
      <h3>filter change</h3>

      <Filter filters={filters} onFilterChange={handleInputChange} onFilterSubmit={handleFilterSubmit}/>
      <h3>Transaction Table</h3>
      <Pagination
        pageNumber={filters.pageNumber}
        pageSize={filters.pageSize}
        onPageChange={handlePageChange}
        onPageSizeChange={handlePageSizeChange}
      />
      
      {transactionLoading && <p>transactionLoading...</p>}
      {getTransactionError && <p>Error: {getTransactionError}</p>}
      {transactionsData.length > 0 && <GenericTable tableHeaders={TableHeaders} data={transactionsData}/>}
      
      </div>
    </Layout>
  )
}

export default App
