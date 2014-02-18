using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;


public class Index_Auto_2fProductModels_2fBy__document_idSortBy__document_id : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fProductModels_2fBy__document_idSortBy__document_id()
	{
		this.ViewText = @"from doc in docs.ProductModels
select new { __document_id = doc.__document_id }";
		this.ForEntityNames.Add("ProductModels");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "ProductModels", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				__document_id = doc.__document_id
			});
		this.AddField("__document_id");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("__document_id");
	}
}
