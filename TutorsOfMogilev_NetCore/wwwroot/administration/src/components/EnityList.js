import _ from 'lodash';
import React, { Component } from 'react';
import EditableList from './EditableList';
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

  handleDeleteStart = (id, text) => {
    this.setState({ changingItemId: id, isEdit: false });
    this.props.openDeleteDialog();
  };

  handleDeleteConfirm = () => {
    this.props.remove(this.state.changingItemId);
    this.props.closeDeleteDialog();
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
      closeInputDialog
    } = this.props;

    const { changingItemId, isEdit } = this.state;

    const description = 'Введите название.';

    const deleteItem = changingItemId !== null && _.find(items, { id: changingItemId });
    const deleteText = deleteItem && deleteItem.name;

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
          text={deleteText}
          handleClose={closeDeleteDialog}
          handleConfirm={this.handleDeleteConfirm}
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

    return (
      <Paper
        style={{
          width: '50%',
          minHeight: '100px',
          margin: '0 auto',
          position: 'relative'
        }}
      >
        {content}
      </Paper>
    );
  }
}
