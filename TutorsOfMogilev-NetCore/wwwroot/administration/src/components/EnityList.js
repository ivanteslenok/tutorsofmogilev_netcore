import _ from 'lodash';
import React, { Component } from 'react';
import EditableList from './EditableList/index';
import DeleteConfirmDialog from './DeleteConfirmDialog';
import DialogWithInput from './DialogWithInput';
import CreateBtn from './CreateBtn';
import Loading from './Loading';
import Paper from '@material-ui/core/Paper';

export default class EnityList extends Component {
  state = {
    changingItemId: null,
    isEdit: false
  };

  componentDidMount() {
    const { loading, loaded, loadData } = this.props;
    if (!loaded && !loading) loadData();
  }

  handleEditStart = id => {
    this.setState({ changingItemId: id, isEdit: true });
    this.props.openInputDialog();
  };

  handleDeleteStart = id => {
    this.setState({ changingItemId: id, isEdit: false });
    this.props.openDeleteDialog();
  };

  handleAccept = value => {
    const { update, create } = this.props;
    const { changingItemId, isEdit } = this.state;

    isEdit ? update(changingItemId, { name: value }) : create({ name: value });

    this.setState({ changingItemId: null, isEdit: false });
  };

  handleEditClose = () => {
    this.setState({ changingItemId: null, isEdit: false });
    this.props.closeInputDialog();
  };

  render() {
    const {
      items,
      loading,
      deleteDialogOpen,
      closeDeleteDialog,
      withInputDialogOpen,
      openInputDialog,
      closeInputDialog,
      remove
    } = this.props;

    const { changingItemId, isEdit } = this.state;

    const description = 'Введите название.';

    const content = loading ? (
      <Loading />
    ) : (
      <>
        <EditableList
          items={items}
          handleItemEdit={this.handleEditStart}
          handleItemDelete={this.handleDeleteStart}
        />
        <div>
          <CreateBtn handleClick={openInputDialog} />
        </div>
        <DeleteConfirmDialog
          isOpen={deleteDialogOpen}
          handleClose={closeDeleteDialog}
          handleConfirm={() => remove(changingItemId)}
        />
        <DialogWithInput
          title={isEdit ? 'Редактирование' : 'Создание'}
          description={description}
          isOpen={withInputDialogOpen}
          handleClose={isEdit ? this.handleEditClose : closeInputDialog}
          inputValue={isEdit ? _.find(items, { id: changingItemId }).name : ''}
          handleAccept={this.handleAccept}
        />
      </>
    );

    return <Paper style={{ width: '50%', margin: '0 auto' }}>{content}</Paper>;
  }
}
