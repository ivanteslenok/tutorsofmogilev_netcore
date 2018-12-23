import React from 'react';
import IconButton from '@material-ui/core/IconButton';
import DeleteIcon from '@material-ui/icons/Delete';
import Tooltip from '@material-ui/core/Tooltip';

export default ({ onExecute }) => (
  <Tooltip title="Удалить" placement="right">
    <IconButton onClick={onExecute}>
      <DeleteIcon />
    </IconButton>
  </Tooltip>
);
