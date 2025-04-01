import React from "react";
import './Filter.css'
export function Filter({ filters, onFilterChange, onFilterSubmit }){

      return (
        <form className="filter-form" onSubmit={onFilterSubmit}>
          <div className="filter-row">
            
    
            <label>
              <span>From Dte</span>
              <input
                type="date"
                name="fromDate"
                value={filters.fromDate}
                onChange={onFilterChange}
              />
            </label>
    
            <label>
              <span>To Date</span>
              <input
                type="date"
                name="toDate"
                value={filters.toDate}
                onChange={onFilterChange}
              />
            </label>
            <label>
              <span>Type of Transaction</span>
              <select name="type" value={filters.type} onChange={onFilterChange}>
                <option value="">-- Select --</option>
                <option value="Deposit">Deposit</option>
                <option value="Withdrawal">Withdrawal</option>
              </select>
            </label>
          </div>
    
          <div className="filter-row">
            <label>
              <span>Sort by</span>
              <select name="sortBy" value={filters.sortBy} onChange={onFilterChange}>
                <option value="">-- Select --</option>
                <option value="Amount">Amount</option>
                <option value="Date">Date</option>
              </select>
            </label>

            <label>
              <span>Descending</span>
              <input
                type="checkbox"
                name="isDescending"
                checked={filters.isDescending || false}
                onChange={onFilterChange}
              />
            </label>
            <label>
              <span>Deleted Transaction</span>
              <input
                type="checkbox"
                name="isDeleted"
                checked={filters.isDeleted || false}
                onChange={onFilterChange}
              />
            </label>
          </div>
    
          <div className="filter-actions">
            <button type="submit">Filter</button>
          </div>
        </form>
      );
    
}