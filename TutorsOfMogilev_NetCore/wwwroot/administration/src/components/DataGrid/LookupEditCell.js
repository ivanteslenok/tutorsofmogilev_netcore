import React from 'react';
import Input from '@material-ui/core/Input';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import TableCell from '@material-ui/core/TableCell';

const LookupEditCell = ({ availableColumnValues, value, onValueChange }) => (
  <TableCell>
    <Select
      value={value}
      onChange={event =>
        onValueChange(
          availableColumnValues.find(x => x.name === event.target.value)
        )
      }
      input={<Input />}
    >
      {availableColumnValues.map(item => (
        <MenuItem key={item.id} value={item.name}>
          {item.name}
        </MenuItem>
      ))}
    </Select>
  </TableCell>
);

export default LookupEditCell;
