import React from 'react';
import EditBtn from '../../EditBtn';
import DeleteBtn from '../../DeleteBtn';

import './index.css';

export default function EditableListItem({
  item,
  handleEdit,
  handleDelete
}) {
  return (
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
}
