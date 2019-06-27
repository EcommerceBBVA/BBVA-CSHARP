using Bbva.Entities;
using Bbva.Entities.Request;
using Bbva.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bbva
{
	/**
	 * <summary>
	 * Clase base que contiene las operaciones disponibles para la administracion de los tokens
	 * </summary>
	 */
	public class TokenService : BbvaResourceService<TokenRequest, Dictionary<String, Object>>
	{

		public TokenService(string api_key, string merchant_id, bool production = false)
			: base(api_key, merchant_id, production)
		{
			ResourceName = "tokens";
		}

		internal TokenService(BbvaHttpClient opHttpClient)
			: base(opHttpClient)
		{
			ResourceName = "tokens";
		}

		public Dictionary<String, Object> Create(List<IParameter> token_request)
		{
			return base.Save(null, token_request);
		}
		
		public Dictionary<String, Object> Get(string id)
		{
			return base.Get(null, id);
		}

	}
}

