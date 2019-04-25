import React from 'react';
import IconButton from '@material-ui/core/IconButton';
import EditIcon from '@material-ui/icons/Edit';
import Tooltip from '@material-ui/core/Tooltip';

const EditBtn = ({ onExecute }) => (
  <Tooltip title="Изменить" placement="left">
    <IconButton onClick={onExecute}>
      <EditIcon />
    </IconButton>
  </Tooltip>
);

export default EditBtn;
