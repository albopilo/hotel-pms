import {useEffect,useState} from 'react';

import {invoiceService} from '@/services/invoiceService';

import type {
 Invoice,
 InvoiceItem,
 Guest,
 Branch,
 Reservation
} from '@/types/database';

import {
 formatIDR,
 formatDateTime
} from '@/lib/format';


interface Props {
 invoiceId:string;
}


export function InvoicePrintPage({
 invoiceId
}:Props){


const [invoice,setInvoice]=useState<Invoice|null>(null);

const [items,setItems]=useState<InvoiceItem[]>([]);

const [guest,setGuest]=useState<Guest|null>(null);

const [branch,setBranch]=useState<Branch|null>(null);

const [reservation,setReservation]=useState<any>(null);



useEffect(()=>{
if(!loading){
    setTimeout(()=>{
      window.print();
    },500);
  }
},[loading]);

async function load(){


 const detail =
 await invoiceService.getInvoiceDetail(invoiceId);


 console.log(
   "PRINT DETAIL:",
   detail
 );


 setInvoice(detail);

 setItems(
   detail.invoice_items || []
 );


 setGuest(
   detail.guests || null
 );


 setBranch(
   detail.branches || null
 );


 setReservation(
   detail.reservations || null
 );


 setTimeout(()=>{

   window.print();

 },800);


}


load();


},[invoiceId]);





if(!invoice){

 return (
  <div className="p-10">
   Loading invoice...
  </div>
 );

}





return (

<>

<style>
{`

@media print {

 body {
   background:white;
 }

 .no-print {
   display:none;
 }

}

`}
</style>



<div className="
max-w-3xl
mx-auto
p-10
bg-white
text-black
">


{/* HEADER */}

<div className="
border-b
pb-5
mb-6
">


<h1 className="
text-3xl
font-bold
">

{branch?.name || 'Hotel'}

</h1>


<p>
{branch?.address || ''}
</p>


</div>





{/* CUSTOMER */}


<div className="
grid
grid-cols-2
mb-8
">


<div>

<h2 className="font-bold">
Bill To
</h2>


<p>
{guest?.full_name || '-'}
</p>


<p>
Room:
{
 reservation?.rooms?.room_number
 ||
 reservation?.room_number
 ||
 reservation?.room_id
 ||
 '-'
}
</p>


</div>




<div className="
text-right
">


<p>
Invoice:
<b>
 {invoice.invoice_number}
</b>
</p>


<p>

{
invoice.issued_at
?
formatDateTime(invoice.issued_at)
:
'-'
}

</p>


</div>



</div>






{/* ITEMS */}


<table className="
w-full
border-collapse
">


<thead>

<tr className="
border-b
">


<th className="
text-left
py-2
">

Description

</th>


<th>
Qty
</th>


<th className="
text-right
">

Amount

</th>


</tr>


</thead>



<tbody>


{
items.map(item=>(


<tr
key={item.id}
className="border-b"
>


<td className="py-2">

{item.description}

</td>


<td className="text-center">

{item.quantity}

</td>


<td className="text-right">

{formatIDR(item.amount)}

</td>


</tr>


))

}


</tbody>


</table>





{/* TOTAL */}


<div className="
mt-8
text-right
">


<p>
Subtotal:
{formatIDR(invoice.subtotal)}
</p>


<p>
Tax:
{formatIDR(invoice.tax)}
</p>



<h2 className="
text-xl
font-bold
mt-2
">

Total:
{formatIDR(invoice.total)}

</h2>



</div>






<div className="
mt-12
text-center
text-xs
">


Thank you for staying with us.


</div>





<button
className="
no-print
mt-8
border
px-4
py-2
rounded
"
onClick={()=>window.print()}
>

Print

</button>



</div>


</>


);


}