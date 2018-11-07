import React from 'react';
import IconBtn from '../../../components/IconBtn';

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
          <IconBtn
            handleClick={handleEdit}
            title="Изменить"
            placement="left"
            icon="edit"
          />
          <IconBtn
            handleClick={handleDelete}
            title="Удалить"
            placement="right"
            icon="delete"
          />
        </div>
      </div>
    </li>
  );
}
