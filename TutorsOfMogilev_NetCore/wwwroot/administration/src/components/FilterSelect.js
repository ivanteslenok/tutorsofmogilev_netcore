import _ from 'lodash';

import React from 'react';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import IconButton from '@material-ui/core/IconButton';
import CancelIcon from '@material-ui/icons/Cancel';
import Tooltip from '@material-ui/core/Tooltip';

const FilterSelect = ({ items, itemId, setItemId, placeholder }) => {
  const onChange = event =>
    setItemId(items.find(x => x.name === event.target.value).id);

  const clear = () => setItemId(null);

  const selectedItem = _.find(items, { id: itemId });

  return (
    <>
      <Select
        value={selectedItem ? selectedItem.name : ''}
        onChange={onChange}
        displayEmpty
      >
        <MenuItem value="" disabled>
          {placeholder}
        </MenuItem>
        {items.map(item => (
          <MenuItem key={item.id} value={item.name}>
            {item.name}
          </MenuItem>
        ))}
      </Select>
      <Tooltip title="Сбросить" placement="down">
        <IconButton color="secondary" onClick={clear}>
          <CancelIcon />
        </IconButton>
      </Tooltip>
    </>
  );
};

export default FilterSelect;
