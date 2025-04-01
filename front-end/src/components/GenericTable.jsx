import './GenericTable.css';

export function GenericTable({ tableHeaders, data }) {
    return (
      <div className='generic-table'>
        <div>
          <table border="1" cellPadding="5" cellSpacing="0">
            <colgroup>
              <col style={{ width: "5%" }} />
              <col style={{ width: "10%" }} />
              <col style={{ width: "10%" }} />
              <col style={{ width: "10%" }} />
              <col style={{ width: "10%" }} />
              <col style={{ width: "10%" }} />
              <col style={{ width: "10%" }} />
              <col style={{ width: "10%" }} />
              <col style={{ width: "5%" }} />
              <col style={{ width: "5%" }} />
              <col style={{ width: "5%" }} />
              <col style={{ width: "5%" }} />
            </colgroup>
            <thead>
              <tr>
                {tableHeaders.map((th, index) => (
                  <th key={index}>{th}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {data.map((item) => (
                <tr key={item.id}>
                  <td>{item.id}</td>
                  <td>{item.hebrewName}</td>
                  <td>{item.englishName}</td>
                  <td>{item.idNumber}</td>
                  <td>{item.accountNumber}</td>
                  <td>{new Date(item.birthDate).toLocaleDateString("en-GB")}</td>
                  <td>{`${item.amount} ₪`}</td>
                  <td>{new Date(item.date).toLocaleDateString("en-GB")}</td>
                  <td>{item.type}</td>
                  <td>{item.status}</td>
                  <td>{item.isEdited ? "Yes" : "No"}</td>
                  <td>{item.isDeleted ? "Yes" : "No"}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    );
  }
  