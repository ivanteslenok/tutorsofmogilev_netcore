import React from 'react';
import EditBtn from '../../EditBtn';
import DeleteBtn from '../../DeleteBtn';

import './index.css';

const EditableListItem = ({ item, handleEdit, handleDelete }) => (
  <li className="editable-list-item">
    <div>
      {item.name}
      <div className="del-btn-block">
        <EditBtn onExecute={handleEdit} />
        <DeleteBtn onExecute={handleDelete} />
      </div>
    </div>
  </li>
);

export default EditableListItem;
