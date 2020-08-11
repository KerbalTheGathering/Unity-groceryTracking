const express = require('express');
const app = express();
const port = 4000;

const low = require('lowdb');
const FileSync = require('lowdb/adapters/FileSync');
const adapter = new FileSync('db.json');
const db = low(adapter);

db.defaults({
    pantryItems: [], groceryLists: [], groceryItems: []
}).write();

app.use(express.json());

app.get('/', (req,res) => {
   res.send(`<h1>Pantry Items</h1>`)
});

app.get('/api/groceryList', (req, res) => {
    res.send(`<h1>Grocery List</h1>`)
});

app.get('/api/grocery', (req, res) => {
   res.send(db.get('groceryLists'))
});

app.get('/api/groceryItems', (req, res) => {
    res.send(db.get('groceryItems').value())
});

app.get('/api/groceryItems/:id', (req, res) => {
    res.send(db.get('groceryItems').value())
});

app.post('/api/grocery/add', (req, res) => {
   const item = {
       id: req.body.id,
       itemName: req.body.itemName,
       itemCount: req.body.itemCount,
       itemPrice: req.body.itemPrice
   };
   console.log(item);
   let compare = db.get('groceryItems')
       .find({id: item.id})
       .value();
   if (!compare){
       compare = db.get('groceryItems')
           .find({itemName: item.itemName})
           .value();
       if (!compare){
           db.get('groceryItems')
               .push(item)
               .write();
           res.send(item);
           return;
       } else {
           db.get('groceryItems')
               .find({itemName: item.itemName})
               .assign({
                   itemCount: item.itemCount,
                   itemPrice: item.itemPrice
               })
               .write();
           res.send(item);
           return;
       }
   } else {
       db.get('groceryItems')
           .find({id: item.id})
           .assign({
               itemName: item.itemName,
               itemCount: item.itemCount,
               itemPrice: item.itemPrice
           }).write();
       res.send(item);
   }
});

app.post('/api/grocery/delete', (req, res) => {
    const item = {
        id: req.body.id,
        itemName: req.body.itemName,
        itemCount: req.body.itemCount,
        itemPrice: req.body.itemPrice
    };
    console.log(item);
    db.get('groceryItems')
        .remove({id: item.id})
        .write();
    res.send(item);
})

app.get('/api/grocery/find/:id', (req, res) => {
   const listObj = db.get('groceryLists')
       .find({id: req.params.id})
       .value();
   if (!listObj){
       res.status(404).send('The item with the supplied Id was not found.');
       return;
   }
   res.send(listObj);
});

app.get('/api/pantry', (req, res) => {
   res.send(db.get('pantryItems'));
})

app.get('/api/pantry/:name', (req, res) => {
   const item = db.get('pantryItems')
       .find({itemName: req.params.name})
       .value();
   if (!item){
      res.status(404).send('The item with the supplied Id was not found.');
      return;
   }
   res.send(item);
});

app.post('/api/pantry/add', (req, res) => {
   const item = {
      id: req.body.id,
      itemName: req.body.itemName,
      quantity: parseFloat(req.body.quantity)
   };
   db.get('pantryItems')
       .push(item)
       .write();
   res.send(item);
});

app.post('/api/pantry/update', (req, res) => {
   const item = {
       id: req.body.id,
       itemName: req.body.itemName,
       quantity: parseFloat(req.body.quantity)
   };
   db.get('pantryItems')
       .find({id: item.id})
       .assign({itemName: item.itemName, quantity: item.quantity})
       .write();
   res.send(item);
});

app.post('/api/pantry/delete', (req, res) => {
   const item = {
       id: req.body.id,
       itemName: req.body.itemName,
       quantity: parseFloat(req.body.quantity)
   };
   db.get('pantryItems')
       .remove({id: item.id})
       .write();
   res.send(item);
});

app.listen(port, () =>
{
   console.log('Listening on port ', port);
});
