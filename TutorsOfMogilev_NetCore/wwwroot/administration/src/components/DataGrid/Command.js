import React from 'react';
import AddRowBtn from './AddRowBtn';
import EditBtn from '../EditBtn';
import DeleteBtn from '../DeleteBtn';
import SaveBtn from '../SaveBtn';
import CancelBtn from '../CancelBtn';

const commandComponents = {
  add: AddRowBtn,
  edit: EditBtn,
  delete: DeleteBtn,
  commit: SaveBtn,
  cancel: CancelBtn
};

export default ({ id, onExecute }) => {
  const CommandButton = commandComponents[id];
  return <CommandButton onExecute={onExecute} />;
};
