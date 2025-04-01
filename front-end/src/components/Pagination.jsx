import React from "react";
import './Pagination.css'
export function Pagination({ pageNumber, pageSize, onPageChange, onPageSizeChange }) {
  return (
    <div className="pagination-container" style={{ marginTop: "1rem" }}>
      
      <button
        disabled={pageNumber <= 1}
        onClick={() => onPageChange(pageNumber - 1)}
      >
        Previous
      </button>

     
      <span style={{ margin: "0 1rem" }}>Page: {pageNumber}</span>

   
      <button onClick={() => onPageChange(pageNumber + 1)}>Next</button>

    
      <label style={{ marginLeft: "1rem" }}>
        Page Size:
        <select
          value={pageSize}
          onChange={(e) => onPageSizeChange(Number(e.target.value))}
          style={{ marginLeft: "0.5rem" }}
        >
          <option value="5">5</option>
          <option value="10">10</option>
          <option value="20">20</option>
          <option value="50">50</option>
        </select>
      </label>
    </div>
  );
}
