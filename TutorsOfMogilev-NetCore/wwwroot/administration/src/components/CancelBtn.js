import React from 'react';
import IconButton from '@material-ui/core/IconButton';
import CancelIcon from '@material-ui/icons/Cancel';
import Tooltip from '@material-ui/core/Tooltip';

export default ({ onExecute }) => (
  <Tooltip title="Отмена" placement="right">
    <IconButton color="secondary" onClick={onExecute}>
      <CancelIcon />
    </IconButton>
  </Tooltip>
);
