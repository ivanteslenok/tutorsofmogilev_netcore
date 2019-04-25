import React from 'react';
import { TableEditRow } from '@devexpress/dx-react-grid-material-ui';
import LookupEditCell from './LookupEditCell';

const EditCell = props => {
  const { column } = props;

  if (column.availableValues) {
    return (
      <LookupEditCell
        {...props}
        availableColumnValues={column.availableValues}
      />
    );
  }

  return <TableEditRow.Cell {...props} />;
};

export default EditCell;
